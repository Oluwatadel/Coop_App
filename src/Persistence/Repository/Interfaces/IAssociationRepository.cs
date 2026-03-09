using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IAssociationRepository
    {
        Task<Association> AddAssociationAsync(Association association, CancellationToken cancellationToken);
        Task<Association> GetAssociationByIdAsync(Guid associationId, CancellationToken cancellationToken);
        Task<Association> GetAssociationByNameAsync(string associationName, CancellationToken cancellationToken);
        Task<Association> GetAllAssociations(CancellationToken cancellationToken);
        Task<Association> UpdateAsscociation(Association association, CancellationToken cancellationToken);
        Task<Association> AsscociationExist(Association association, CancellationToken cancellationToken);
    }
}
