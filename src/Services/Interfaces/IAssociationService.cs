using CoopApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Services.Interfaces
{
    public interface IAssociationService
    {
        Task<Association> CreateAssociationAsync(string name, string? description, CancellationToken cancellationToken);
        Task<Association> UpdateAssociationAsync(Guid associationId, string name, CancellationToken cancellationToken);
        Task<Association> GetAssociationByNameAsync(string name, CancellationToken cancellationToken);
        Task<Association> GetAssociationByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Association>> GetAllAssociation(CancellationToken cancellationToken);
    }
}
