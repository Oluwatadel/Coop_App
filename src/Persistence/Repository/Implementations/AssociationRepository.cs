using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class AssociationRepository(CoopDbContext context) : IAssociationRepository
    {
        public async Task<Association> AddAssociationAsync(Association association, CancellationToken cancellationToken)
        {
            await context.Associations.AddAsync(association, cancellationToken);
            return association;
        }

        public async Task<bool> AsscociationExist(Guid associationId, CancellationToken cancellationToken)
        {
            var exist = context.Associations.AsQueryable();
            var any = await exist.AnyAsync(a => a.Id == associationId, cancellationToken);
            return any;
        }
        
        public async Task<bool> AsscociationExistWithName(string name, CancellationToken cancellationToken)
        {
            var exist = context.Associations.AsQueryable();
            var any = await exist.AnyAsync(a => a.Name == name, cancellationToken);
            return any;
        }

        public async Task<IReadOnlyList<AssociationDto>> GetAllAssociations(CancellationToken cancellationToken)
        {
            var query = from association in context.Associations
                        join user in context.Users
                        on association.Id equals user.AssociationId into userGroup
                        let userId = userGroup.Select(u => u.Id)
                        let loans = context.LoanTaken.Where(l => userId.Contains(l.UserId))
                        select new AssociationDto(
                    association.Id,
                    association.Name,
                    association.Description,
                    userGroup.Count(),
                    new AssociationLoanSummary(
                        loans.Count(),
                        loans.Sum(l => (decimal?)l.PrincipalAmount) ?? 0,
                        loans.Sum(l => (decimal?)l.BalanceRemaining) ?? 0,
                        context.Accounts.Where(a => userId.Contains(a.UserId))
                            .Sum(a => (decimal?)a.SavingsBalance) ?? 0
                    )
                );

            var data = await query.ToListAsync(cancellationToken);

            return data;
        }

        public async Task<Association> GetAssociationByIdAsync(Guid associationId, CancellationToken cancellationToken)
        {
            var data = await context.Associations
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == associationId, cancellationToken);
            return data;
        }

        public Task<Association> GetAssociationByNameAsync(string associationName, CancellationToken cancellationToken)
        {
            var data = context.Associations
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Name == associationName, cancellationToken);
            return data;
        }

        public Association UpdateAsscociation(Association association)
        {
            context.Associations.Update(association);
            return association;
        }
    }
}
