using CoopApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> CreateRoleAsynce(Role role, CancellationToken cancellationToken);
        Task<Role?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);
        Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
        Task<bool> ExistAsync(string roleName, CancellationToken cancellationToken);
        Task<IReadOnlyList<Role>> GetAllRoleAsync(CancellationToken cancellationToken);
        Role UpdateRole(Role role);

    }
}
