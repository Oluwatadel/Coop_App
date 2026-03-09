using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Services.Implementations
{
    public class AssociationService(IUnitofWork unitofWork, IAssociat) : IAssociationService
    {
        public Task<Association> CreateAssociationAsync(string name, string description)
        {
            var association = new Association(name, description);
            var changes = 
        }

        public Task<IReadOnlyList<Domain.Entities.Association>> GetAllAssociation(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Association> GetAssociationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Association> GetAssociationByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Association> UpdateAssociationAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
