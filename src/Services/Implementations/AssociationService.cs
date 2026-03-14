using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using System.Data;

namespace CoopApplication.Services.Implementations
{
    public class AssociationService(IUnitofWork unitofWork, IAssociationRepository associationRepository) : IAssociationService
    {
        public async Task<Association?> CreateAssociationAsync(string name, string description, CancellationToken cancellationToken)
        {
            var association = new Association(name, description);
            var exist = await associationRepository.AsscociationExistWithName(name, cancellationToken);
            if(exist)
            {
                throw new AlreadyExistsException($"Association exist with name {name}");
            }
            var createdAssociation = await associationRepository.AddAssociationAsync(association, cancellationToken);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if (changes <= 0)
            {
                throw new SaveOperationException("Failed to save the new association to the database.");
            }
            return createdAssociation;
        }

        public async Task<IReadOnlyList<AssociationDto>> GetAllAssociation(CancellationToken cancellationToken)
        {
            var associations = await associationRepository.GetAllAssociations(cancellationToken);
            return associations;
        }

        public async Task<Association> GetAssociationByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var association = await associationRepository.GetAssociationByIdAsync(id, cancellationToken);
            if(association == null)
            {
                throw new NotFoundException($"Association with ID {id} not found.");
            }
            return association;
        }

        public Task<Association> GetAssociationByNameAsync(string name, CancellationToken cancellationToken)
        {
            var association = associationRepository.GetAssociationByNameAsync(name, cancellationToken);
            if(association == null)
            {
                throw new NotFoundException($"Association with name {name} not found.");
            }
            return association;
        }

        public async Task<Association> UpdateAssociationAsync(Guid associationId, string name, CancellationToken cancellationToken)
        {
            var association = await associationRepository.GetAssociationByIdAsync(associationId, cancellationToken);
            if(association == null)
            {
                throw new NotFoundException($"Association with ID {associationId} not found.");
            }
            association.Name = name;
            var updatedAssociation = associationRepository.UpdateAsscociation(association);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if (changes <= 0)
            {
                throw new SaveOperationException("Failed to save the updated association to the database.");
            }
            return updatedAssociation;
        }
    }
}
