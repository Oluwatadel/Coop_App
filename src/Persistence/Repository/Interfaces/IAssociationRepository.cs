using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IAssociationRepository
    {
        Task<Association> AddAssociationAsync(Association association, CancellationToken cancellationToken);
        Task<Association> GetAssociationByIdAsync(Guid associationId, CancellationToken cancellationToken);
        Task<Association> GetAssociationByNameAsync(string associationName, CancellationToken cancellationToken);
        Task<IReadOnlyList<Association>> GetAllAssociations(CancellationToken cancellationToken);
        Association UpdateAsscociation(Association association);
        Task<bool> AsscociationExist(Guid associationId, CancellationToken cancellationToken);
    }
}
