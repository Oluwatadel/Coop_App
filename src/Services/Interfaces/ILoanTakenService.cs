using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;

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
