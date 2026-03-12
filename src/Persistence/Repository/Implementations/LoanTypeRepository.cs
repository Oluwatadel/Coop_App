using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class LoanTypeRepository(CoopDbContext context) : ILoanTypeRepository
    {
        public async Task<LoanType> CreateLoanTypeAsync(LoanType loanType, CancellationToken cancellationToken)
        {
            await context.LoanTypes.AddAsync(loanType, cancellationToken);
            return loanType;
        }

        public async Task<LoanType?> GetLoanTypeByIdAsync(Guid loanTypeId, CancellationToken cancellationToken)
        {
            return await context.LoanTypes
                .FirstOrDefaultAsync(lt => lt.Id == loanTypeId, cancellationToken);
        }

        public async Task<LoanType?> GetLoanTypeByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await context.LoanTypes
                .FirstOrDefaultAsync(lt => lt.Name == name, cancellationToken);
        }

        public async Task<IReadOnlyList<LoanType>> GetAllLoanTypesAsync(CancellationToken cancellationToken)
        {
            return await context.LoanTypes.ToListAsync(cancellationToken);
        }

        public LoanType UpdateLoanType(LoanType loanType)
        {
            context.LoanTypes.Update(loanType);
            return loanType;
        }
    }
}
