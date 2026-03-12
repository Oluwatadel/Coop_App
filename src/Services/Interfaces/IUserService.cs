using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;

namespace CoopApplication.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserRequest user, CancellationToken cancellationToken);
        Task<UserResponse?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<UserResponse?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserResponse?>> SearchUserAsync(SearchUser request, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserResponse?>> GetAllUsersAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<UserResponse>> GetMembersOfAnAssociation(Guid associationId, CancellationToken cancellationToken);
        Task<UserResponse> UserExistAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<UserResponse> AssignRoleAsync(Guid roleId, Guid userId, CancellationToken cancellationToken);
    }
}
