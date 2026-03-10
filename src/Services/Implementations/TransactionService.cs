using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Repository.Implementations;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using CoopApplication.Domain.Entities;


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
           var transactions = await _transactionRepository.GetTransactionByIdAsync(transactionId, cancellationToken);
            if (transactions == null)
                return null;
            return new TransactionResponse
            {
                Id = transactions.Id,
                UserId = transactions.UserId,
                Amount = transactions.Amount,
                PaymentMethod = transactions.PaymentMethod,
                TransactionType = transactions.TransactionType,
                Date = transactions.Date
            };
        }

        public async Task<IReadOnlyList<TransactionResponse>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId, cancellationToken);
            return transactions.Select(t => new TransactionResponse
            { Id = t.Id,
            UserId = t.UserId,
            Amount = t.Amount,
            PaymentMethod = t.PaymentMethod,
            TransactionType = t.TransactionType,
            Date = t.Date}).ToList();
        }

        public async Task<TransactionResponse> ProcessTransactionAsync(
            TransactionRequestDto request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                throw new ArgumentException("Transaction amount must be greater than 0");

            decimal remainingAmount = request.Amount;

            var transactions = new Transaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionType = request.TransactionType,
                Date = request.Date
            };

            await _transactionRepository.CreateTransactionAsync(transactions, cancellationToken);

            var loans = await _loanTakenRepository
                .GetLoanTakenByUserIdAsync(request.UserId, cancellationToken);

            var orderedLoans = loans
                .Where(l => l.Status == LoanStatus.Approved || l.Status == LoanStatus.Ongoing)
                .OrderBy(l => l.StartDate)
                .ToList();

            foreach (var loan in orderedLoans)
            {
                if (remainingAmount <= 0)
                    break;

                var paymentAmount = Math.Min(loan.BalanceRemaining, remainingAmount);

                var repayment = new LoanRepayment
                {
                    Id = Guid.NewGuid(),
                    LoanId = loan.Id,
                    Amount = paymentAmount,
                    Date = DateTime.UtcNow
                };

                loan.RepaymentTransaction(repayment);

                await _loanRepaymentRepository.CreateLoanRepaymentAsync(
                    repayment,
                    cancellationToken);

                _loanTakenRepository.UpdateLoanTaken(loan);

                remainingAmount -= paymentAmount;
            }

            if (remainingAmount > 0)
            {
                var account = await _accountRepository
                    .GetAccountByUserIdAsync(request.UserId, cancellationToken);

                if (account == null)
                    throw new Exception("User account not found");

                account.TotalShares += remainingAmount;

                var savingsTransaction = new Transaction
                {
                    UserId = request.UserId,
                    Amount = remainingAmount,
                    PaymentMethod = request.PaymentMethod,
                    TransactionType = TransactionType.SavingsDeposit,
                    Date = DateTime.UtcNow
                };

                await _transactionRepository.CreateTransactionAsync(
                    savingsTransaction,
                    cancellationToken);
            }

            await _unitofwork.SaveChanges(cancellationToken);

            return new TransactionResponse
            {
                Id = transactions.Id,
                UserId = transactions.UserId,
                Amount = transactions.Amount,
                PaymentMethod = transactions.PaymentMethod,
                TransactionType = transactions.TransactionType,
                Date = transactions.Date
            };
        }
    }
}
