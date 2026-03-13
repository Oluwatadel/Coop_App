using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;

namespace CoopApplication.Services.Interfaces
{
    public interface IAssociationService
    {
        Task<Association?> CreateAssociationAsync(string name, string description, CancellationToken cancellationToken);
        Task<Association> UpdateAssociationAsync(Guid associationId, string name, CancellationToken cancellationToken);
        Task<Association> GetAssociationByNameAsync(string name, CancellationToken cancellationToken);
        Task<Association> GetAssociationByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<AssociationDto>> GetAllAssociation(CancellationToken cancellationToken);
    }
}
