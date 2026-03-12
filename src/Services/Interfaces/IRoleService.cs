using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Services.Interfaces
{
    public interface IRoleService
    {
            Task<RoleDto> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken);
            Task<RoleDto?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);
            Task<RoleDto?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
            Task<IReadOnlyList<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken);
            Task<RoleDto> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request, CancellationToken cancellationToken);
    }

}
