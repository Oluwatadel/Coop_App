using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface ILoanTypeRepository
    {
        Task<LoanType> CreateLoanTypeAsync(LoanType loanType, CancellationToken cancellationToken);
        Task<LoanType?> GetLoanTypeByIdAsync(Guid loanTypeId, CancellationToken cancellationToken);
        Task<LoanType?> GetLoanTypeByNameAsync(string name, CancellationToken cancellationToken);
        Task<IReadOnlyList<LoanType>> GetAllLoanTypesAsync(CancellationToken cancellationToken);
        LoanType UpdateLoanType(LoanType loanType);
    }
}
