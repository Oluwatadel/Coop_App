using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface ILoanRepaymentRepository
    {
        Task<LoanRepayment> CreateLoanRepaymentAsync(LoanRepayment loanRepayment, CancellationToken cancellationToken);
        Task<IReadOnlyList<LoanRepayment>> GetLoanRepaymentsByLoanIdAsync(Guid loanId, CancellationToken cancellationToken);
        Task<LoanRepayment?> GetLoanRepaymentByIdAsync(Guid repaymentId, CancellationToken cancellationToken);
    }
}
