using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Services.Interfaces
{
    public interface ILoanTypeService
    {
        Task<LoanTypeResponse> CreateLoanTypeAsync(CreateLoanTypeRequest loanTypeRequest, CancellationToken cancellationToken);
        Task<LoanTypeResponse> UpdateTypeAsync(Guid loanTypeId,
            UpdateLoanTypeRequest loanTypeRequest, CancellationToken cancellationToken);
        Task<LoanTypeResponse> GetLoanTypeAsync(Guid loanTypeId, CancellationToken cancellationToken);
        Task<LoanTypeResponse> GetLoanTypeByNameAsync(string loanName, CancellationToken cancellationToken);
        Task<IReadOnlyList<LoanTypeResponse>> GetAllLoanTpesAsync(CancellationToken cancellationToken);

    }

    
    
}
