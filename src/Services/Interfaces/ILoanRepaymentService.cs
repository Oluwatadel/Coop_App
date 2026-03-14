using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Services.Interfaces
{
    public interface ILoanRepaymentService
    {
        Task<IReadOnlyList<LoanRepaymentResponse>> GetRepaymentsByLoanIdAsync(
            Guid loanId,
            CancellationToken cancellationToken);

        Task<LoanRepaymentResponse?> GetRepaymentByIdAsync(
            Guid repaymentId,
            CancellationToken cancellationToken);
    }
}