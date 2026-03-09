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
        Task<Association> CreateAssociationAsync(string name, string? description);
        Task<Association> UpdateAssociationAsync(string name);
        Task<Association> GetAssociationByNameAsync(string name);
        Task<Association> GetAssociationByIdAsync(Guid id);
        Task<IReadOnlyList<Association>> GetAllAssociation(Guid id);
    }
}
