using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Repository.Implementations;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using Microsoft.VisualBasic;
using System.Security.Principal;


namespace CoopApplication.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILoanRepaymentRepository _loanRepaymentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ILoanTakenRepository _loanTakenRepository;
        private readonly IUnitofWork _unitofwork;

        public TransactionService(ITransactionRepository transactionRepository,
            ILoanRepaymentRepository loanRepaymentRepository, IAccountRepository accountRepository,
            IUnitofWork unitofwork, ILoanTakenRepository loanTakenRepository)
        {
            _transactionRepository = transactionRepository;
            _loanRepaymentRepository = loanRepaymentRepository;
            _accountRepository = accountRepository; 
            _loanTakenRepository = loanTakenRepository;
            _unitofwork = unitofwork;
        }

        public async Task<IReadOnlyList<TransactionResponse>> GetAllTransactionsAsync(CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetAllTransactionsAsync(cancellationToken);

            return transactions.Select(t => new TransactionResponse
            {
                ReferenceNo = t.TransactionReferenceNo,
                Id = t.Id,
                UserId = t.UserId,
                Amount = t.Amount,
                PaymentMethod = t.PaymentMethod,
                TransactionType = t.TransactionType,
                Date = t.Date
            }).ToList();
        }

        public async Task<TransactionResponse?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken)
        {
           var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId, cancellationToken);
            if (transaction == null)
                throw new NotFoundException("Transaction not found");
            return new TransactionResponse
            {
                ReferenceNo = transaction.TransactionReferenceNo,
                Id = transaction.Id,
                UserId = transaction.UserId,
                Amount = transaction.Amount,
                PaymentMethod = transaction.PaymentMethod,
                TransactionType =  transaction.TransactionType,
                Date = transaction.Date
            };
        }

        public async Task<IReadOnlyList<TransactionResponse>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId, cancellationToken);
            return transactions.Select(t => new TransactionResponse
            {
                ReferenceNo = t.TransactionReferenceNo,
                Id = t.Id,
                UserId = t.UserId,
                Amount = t.Amount,
                PaymentMethod = t.PaymentMethod,
                TransactionType = t.TransactionType,
                Date = t.Date
            }).ToList();
        }

        public async Task<TransactionResponse> ProcessTransactionAsync(
    TransactionRequestDto request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                throw new TransactionAmountException("Transaction amount must be greater than 0");

            decimal remainingAmount = request.Amount;

            var transaction = new Transaction
            {
                UserId = request.UserId,
                TransactionReferenceNo = request.TransactionReferenceNo,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionType = request.TransactionType,
                Date = DateTime.UtcNow
            };

            // Get or create account
            var account = await _accountRepository
                .GetAccountByUserIdAsync(request.UserId, cancellationToken);

            if (account == null)
            {
                var newAccount = new Account
                {
                    UserId = request.UserId,
                    SavingsBalance = 0
                };

                account = await _accountRepository
                    .CreateAccountAsync(newAccount, cancellationToken);
            }

            if (request.TransactionType == TransactionType.LoanRepayment)
            {
                var loans = await _loanTakenRepository
                    .GetLoanTakenByUserIdAsync(request.UserId, cancellationToken);

                var orderedLoans = loans
                    .Where(l => l.Status == LoanStatus.Approved || l.Status == LoanStatus.Ongoing)
                    .OrderBy(l => l.StartDate)
                    .ToList();

                // If no active loans → deposit to savings
                if (!orderedLoans.Any())
                {
                    account.SavingsBalance += remainingAmount;
                    _accountRepository.UpdateAccount(account);
                    transaction.TransactionType = TransactionType.SavingsDeposit;
                    remainingAmount = 0;
                }
                else
                {
                    // Repay loans oldest first
                    foreach (var loan in orderedLoans)
                    {
                        if (remainingAmount <= 0)
                            break;

                        var paymentAmount = Math.Min(loan.BalanceRemaining, remainingAmount);

                        var repayment = new LoanRepayment
                        {
                            LoanId = loan.Id,
                            Amount = paymentAmount,
                            Date = DateTime.UtcNow
                        };

                        loan.RepaymentTransaction(repayment);

                        await _loanRepaymentRepository
                            .CreateLoanRepaymentAsync(repayment, cancellationToken);

                        _loanTakenRepository.UpdateLoanTaken(loan);

                        remainingAmount -= paymentAmount;
                    }

                    // Handle remaining amount
                    if (remainingAmount > 0)
                    {
                        var nextLoan = orderedLoans
                            .FirstOrDefault(l => l.Status == LoanStatus.Ongoing && l.BalanceRemaining > 0);

                        if (nextLoan != null && account.SavingsBalance >= 500000m)
                        {
                            var extraRepayment = new LoanRepayment
                            {
                                LoanId = nextLoan.Id,
                                Amount = remainingAmount,
                                Date = DateTime.UtcNow
                            };

                            nextLoan.RepaymentTransaction(extraRepayment);

                            await _loanRepaymentRepository
                                .CreateLoanRepaymentAsync(extraRepayment, cancellationToken);

                            _loanTakenRepository.UpdateLoanTaken(nextLoan);

                            remainingAmount = 0;
                        }
                        else
                        {
                            account.SavingsBalance += remainingAmount;
                            _accountRepository.UpdateAccount(account);
                        }
                    }
                }
            }

            // Save main transaction
            var createdTransaction = await _transactionRepository
                .CreateTransactionAsync(transaction, cancellationToken);

            await _unitofwork.SaveChanges(cancellationToken);

            return new TransactionResponse
            {
                ReferenceNo = createdTransaction.TransactionReferenceNo,
                Id = createdTransaction.Id,
                UserId = createdTransaction.UserId,
                Amount = createdTransaction.Amount,
                PaymentMethod = createdTransaction.PaymentMethod,
                TransactionType = createdTransaction.TransactionType,
                Date = createdTransaction.Date
            };
        }
    }
}
