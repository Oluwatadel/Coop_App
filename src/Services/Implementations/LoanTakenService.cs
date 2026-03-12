using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;

namespace CoopApplication.Services.Implementations
{
    public class LoanTakenService : ILoanTakenService
    {
        private readonly ILoanTakenRepository _loanTakenRepository;
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitofWork _unitOfWork;

        public LoanTakenService(
            ILoanTakenRepository loanTakenRepository,
            ILoanTypeRepository loanTypeRepository,
            IUserRepository userRepository,
            IUnitofWork unitOfWork)
        {
            _loanTakenRepository = loanTakenRepository;
            _loanTypeRepository = loanTypeRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoanTakenResponse> RequestLoanAsync(
         LoanTakenRequest request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                throw new NotFoundException($"User with Id {request.UserId} not found");

            var loanType = await _loanTypeRepository.GetLoanTypeByIdAsync(request.LoanTypeId, cancellationToken);

            if (loanType == null)
                throw new NotFoundException($"Loan type with Id {request.LoanTypeId} not found");

            var loan = new LoanTaken(
                request.UserId,
                loanType,
                request.PrincipalAmount,
                LoanStatus.Pending,
                request.StartDate,
                request.EndDate
            );

            var createdLoan = await _loanTakenRepository.CreateLoanTakenAsync(loan, cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);

            return new LoanTakenResponse
            {
                Id = createdLoan.Id,
                UserId = createdLoan.UserId,
                LoanTypeName = createdLoan.LoanType.Name,
                PrincipalAmount = createdLoan.PrincipalAmount,
                TotalRepaymentAmount = createdLoan.TotalRepaymentAmount,
                MonthlyPaymentAmount = createdLoan.MonthlyPaymentAmount,
                BalanceRemaining = createdLoan.BalanceRemaining,
                Status = createdLoan.Status,
                StartDate = createdLoan.StartDate,
                EndDate = createdLoan.EndDate
            };
        }

        public async Task<LoanTakenResponse> ApproveLoanAsync(
        Guid loanId,Guid approverId,CancellationToken cancellationToken)
        {
            var loan = await _loanTakenRepository.GetLoanTakenByIdAsync(loanId, cancellationToken);

            if (loan == null)
                throw new NotFoundException($"Loan with Id {loanId} not found");

            loan.ApproveLoan(DateTime.UtcNow, approverId);

            _loanTakenRepository.UpdateLoanTaken(loan);

            await _unitOfWork.SaveChanges(cancellationToken);

            return new LoanTakenResponse
            {
                Id = loan.Id,
                UserId = loan.UserId,
                LoanTypeName = loan.LoanType.Name,
                PrincipalAmount = loan.PrincipalAmount,
                TotalRepaymentAmount = loan.TotalRepaymentAmount,
                MonthlyPaymentAmount = loan.MonthlyPaymentAmount,
                BalanceRemaining = loan.BalanceRemaining,
                Status = loan.Status,
                StartDate = loan.StartDate,
                EndDate = loan.EndDate
            };
        }

        public async Task<LoanTakenResponse?> GetLoanByIdAsync(
            Guid loanId,
            CancellationToken cancellationToken)
        {
            var loan = await _loanTakenRepository.GetLoanTakenByIdAsync(loanId, cancellationToken);

            if (loan == null)
                return null;

            return MapToResponse(loan);
        }

        public async Task<IReadOnlyList<LoanTakenResponse>> GetLoansByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var loans = await _loanTakenRepository.GetLoanTakenByUserIdAsync(userId, cancellationToken);

            return loans.Select(MapToResponse).ToList();
        }

        public async Task<IReadOnlyList<LoanTakenResponse>> GetAllLoansAsync(
            CancellationToken cancellationToken)
        {
            var loans = await _loanTakenRepository.GetAllLoanTakenAsync(cancellationToken);

            return loans.Select(MapToResponse).ToList();
        }

        private static LoanTakenResponse MapToResponse(LoanTaken loan)
        {
            return new LoanTakenResponse
            {
                Id = loan.Id,
                UserId = loan.UserId,
                LoanTypeName = loan.LoanType.Name,
                PrincipalAmount = loan.PrincipalAmount,
                TotalRepaymentAmount = loan.TotalRepaymentAmount,
                MonthlyPaymentAmount = loan.MonthlyPaymentAmount,
                BalanceRemaining = loan.BalanceRemaining,
                Status = loan.Status,
                StartDate = loan.StartDate,
                EndDate = loan.EndDate
            };
        }
    }
}