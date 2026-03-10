using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IReadOnlyList<Association>> GetAllAssociations(CancellationToken cancellationToken)
        {
            var data = await context.Associations.ToListAsync(cancellationToken);
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
