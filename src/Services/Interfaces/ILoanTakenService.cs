using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Enums;

namespace CoopApplication.Services.Interfaces
{
    public interface  ILoanTakenService
    {
        Task<LoanTakenResponse> RequestLoanAsync(
         LoanTakenRequest request,CancellationToken cancellationToken);

        Task<LoanTakenResponse> ApproveLoanAsync(
            Guid loanId,Guid approverId,CancellationToken cancellationToken);

        Task<LoanTakenResponse?> GetLoanByIdAsync(
            Guid loanId,CancellationToken cancellationToken);

        Task<IReadOnlyList<LoanTakenResponse>> GetLoansByUserIdAsync(
            Guid userId,CancellationToken cancellationToken);

        Task<IReadOnlyList<LoanTakenResponse>> GetAllLoansAsync(
            CancellationToken cancellationToken);
    }
}
