using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class LoanTakenRepository(CoopDbContext context) : ILoanTakenRepository
    {
        public async Task<LoanTaken> CreateLoanTakenAsync(LoanTaken loanTaken, CancellationToken cancellationToken)
        {
            await context.LoanTaken.AddAsync(loanTaken, cancellationToken);
            return loanTaken;
        }

        public async Task<LoanTaken?> GetLoanTakenByIdAsync(Guid loanId, CancellationToken cancellationToken)
        {
            return await context.LoanTaken
                .FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);
        }

        public async Task<IReadOnlyList<LoanTaken>> GetLoanTakenByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await context.LoanTaken
                .Where(l => l.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<LoanTaken>> GetAllLoanTakenAsync(CancellationToken cancellationToken)
        {
            return await context.LoanTaken.ToListAsync(cancellationToken);
        }

        public LoanTaken UpdateLoanTaken(LoanTaken loanTaken)
        {
            context.LoanTaken.Update(loanTaken);
            return loanTaken;
        }
    }
}
