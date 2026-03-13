using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Services.Interfaces
{
    public interface ILoanRepaymentService
    {
        Task<IReadOnlyList<RepaymentResponse>> GetRepaymentsByLoanIdAsync(
            Guid loanId,
            CancellationToken cancellationToken);

        Task<RepaymentResponse?> GetRepaymentByIdAsync(
            Guid repaymentId,
            CancellationToken cancellationToken);
    }
}