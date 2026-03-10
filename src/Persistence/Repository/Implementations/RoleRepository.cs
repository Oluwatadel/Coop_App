using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CoopApplication.Persistence.Repository.Implementations
{
    public class RoleRepository(CoopDbContext context) : IRoleRepository
    {
        public async Task<Role> CreateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            await context.Roles.AddAsync(role, cancellationToken);
            return role;
        }

        public async Task<IReadOnlyList<Role>> GetAllRoleAsync(CancellationToken cancellationToken)
        {
            var roles = context.Roles.AsQueryable();
            return await roles.ToListAsync(cancellationToken);
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
            return role;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            return role;
        }
        public async Task<bool> ExistAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await context.Roles.AnyAsync(r => r.Name == roleName, cancellationToken);
            return role;
        }

        public Role UpdateRole(Role role)
        {
            context.Roles.Update(role);
            return role;
        }

    }
}
