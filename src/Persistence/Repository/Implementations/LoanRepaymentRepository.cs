using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class LoanRepaymentRepository(CoopDbContext context) : ILoanRepaymentRepository
    {
        public async Task<LoanRepayment> CreateLoanRepaymentAsync(LoanRepayment loanRepayment, CancellationToken cancellationToken)
        {
            await context.LoanRepayments.AddAsync(loanRepayment, cancellationToken);
            return loanRepayment;
        }

        public async Task<IReadOnlyList<LoanRepayment>> GetLoanRepaymentsByLoanIdAsync(Guid loanId, CancellationToken cancellationToken)
        {
            return await context.LoanRepayments
                .Where(r => r.LoanId == loanId)
                .ToListAsync(cancellationToken);
        }

        public async Task<LoanRepayment?> GetLoanRepaymentByIdAsync(Guid repaymentId, CancellationToken cancellationToken)
        {
            return await context.LoanRepayments
                .FirstOrDefaultAsync(r => r.Id == repaymentId, cancellationToken);
        }
    }
}
