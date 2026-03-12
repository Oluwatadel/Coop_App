using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface ILoanTakenRepository
    {
        Task<LoanTaken> CreateLoanTakenAsync(LoanTaken loanTaken, CancellationToken cancellationToken);
        Task<LoanTaken?> GetLoanTakenByIdAsync(Guid loanId, CancellationToken cancellationToken);
        Task<IReadOnlyList<LoanTaken>> GetLoanTakenByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IReadOnlyList<LoanTaken>> GetAllLoanTakenAsync(CancellationToken cancellationToken);
        LoanTaken UpdateLoanTaken(LoanTaken loanTaken);
    }
}
