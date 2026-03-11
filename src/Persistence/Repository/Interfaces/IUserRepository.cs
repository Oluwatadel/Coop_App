using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Implementations;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface  IUserRepository
    {
        Task<UserResponse> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistAsync(string email, string? phoneNumber, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken);
        UserResponse UpdateUser(User user);
        Task<IReadOnlyList<UserResponse?>> SearchUserAsync(SearchUser request, CancellationToken cancellationToken);
        Task<IReadOnlyList<UserResponse>> GetMembersOfAnAssociation(Guid associationId, CancellationToken cancellationToken);
        Task<UserDashBoardOverviewDto?> GetUserDashBoardOverviewDto(Guid userId, CancellationToken cancellationToken);
    }
}
