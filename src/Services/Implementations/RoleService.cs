using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using Mapster;

namespace CoopApplication.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitofWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitofWork unitofWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitofWork;
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var newRole = new Role(request.Name);
            var exist = await _roleRepository.ExistAsync(request.Name, cancellationToken);
            if (exist)
                throw new AlreadyExistException($"A role with {request.Name} already exist");
            var createdRole = await _roleRepository.CreateRoleAsynce(newRole, cancellationToken);
            var changes = await _unitOfWork.SaveChanges(cancellationToken);
            if(changes == 0)
            {
                throw new SaveOperationException("Failed to create role.");
            }
            return new RoleDto(

                Id: createdRole.Id,
                Name: createdRole.Name
                );

        }

        public async Task<IReadOnlyList<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllRoleAsync(cancellationToken);
            return [..roles.Select(r => r.Adapt<RoleDto>())];
        }

        public async Task<RoleDto?> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Role with ID: {roleId} does not exist");
            }
            return role.Adapt<RoleDto>();
        }

        public async Task<RoleDto?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Role with name: {roleName} does not exist");
            }
            return role.Adapt<RoleDto>();
        }

        public async Task<RoleDto> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                throw new NotFoundException($"Role with ID: {roleId} does not exist");
            }

            role.Name = request.Name;
            _roleRepository.UpdateRole(role);
            var changes = await _unitOfWork.SaveChanges(cancellationToken);
            if (changes <= 0)
                throw new SaveOperationException("Error updating role");
            return role.Adapt<RoleDto>();
        }
    }
}
