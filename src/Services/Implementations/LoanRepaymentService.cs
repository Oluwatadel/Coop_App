using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;

namespace CoopApplication.Services.Implementations
{
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly ILoanRepaymentRepository _loanRepaymentRepository;

        public LoanRepaymentService(ILoanRepaymentRepository loanRepaymentRepository)
        {
            _loanRepaymentRepository = loanRepaymentRepository;
        }

        public async Task<IReadOnlyList<LoanRepaymentResponse>> GetRepaymentsByLoanIdAsync(
            Guid loanId,
            CancellationToken cancellationToken)
        {
            var repayments = await _loanRepaymentRepository
                .GetLoanRepaymentsByLoanIdAsync(loanId, cancellationToken);

            return repayments.Select(r => new LoanRepaymentResponse(
                r.Id,
                r.LoanId,
                r.TransactionId,
                r.Amount,
                r.Date,
                r.CreatedAt,
                r.CreatedBy
            )).ToList();
        }

        public async Task<LoanRepaymentResponse?> GetRepaymentByIdAsync(
            Guid repaymentId,
            CancellationToken cancellationToken)
        {
            var repayment = await _loanRepaymentRepository
                .GetLoanRepaymentByIdAsync(repaymentId, cancellationToken);

            if (repayment == null)
                return null;

            return new LoanRepaymentResponse(
                repayment.Id,
                repayment.LoanId,
                repayment.TransactionId,
                repayment.Amount,
                repayment.Date,
                repayment.CreatedAt,
                repayment.CreatedBy
            );
        }
    }
}