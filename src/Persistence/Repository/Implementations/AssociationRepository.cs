using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
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

        public Task<Association> AsscociationExist(Association association, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Association> GetAllAssociations(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Association> GetAssociationByIdAsync(Guid associationId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Association> GetAssociationByNameAsync(string associationName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Association> UpdateAsscociation(Association association, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
