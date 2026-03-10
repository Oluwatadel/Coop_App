using CoopApplication.api.Exceptions;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Interfaces;
using Mapster;

namespace CoopApplication.Services.Implementations
{
    public class UserService(IUserRepository userRepository, IUnitofWork unitofWork, IRoleRepository roleRepository) : IUserService
    {
        public async Task<UserResponse> CreateUserAsync(UserRequest userRequest, CancellationToken cancellationToken)
        {
            var newUser = userRequest.Adapt<User>();
            var returnedUser = userRepository.CreateUserAsync(newUser, cancellationToken);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if (changes <= 0)
            {
                throw new SaveOperationException("Something went wrong while saving the user. Please try again.");
            }
            return returnedUser.Adapt<UserResponse>();

        }

        public async Task<IReadOnlyList<UserResponse?>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await userRepository.GetAllUsersAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserResponse>> GetMembersOfAnAssociation(Guid associationId, CancellationToken cancellationToken)
        {
            return await userRepository.GetMembersOfAnAssociation(associationId, cancellationToken);

        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmailAsync(email, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User with the provided email does not exist.");
            }
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                UserId = user.Id,
            };
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User with the provided email does not exist.");
            }
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive,
                UserId = user.Id,
            };
        }

        public Task<IReadOnlyList<UserResponse?>> SearchUserAsync(SearchUser request, CancellationToken cancellationToken)
        {
            var users = userRepository.SearchUserAsync(request, cancellationToken);
            return users;
        }

        public async Task<UserResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User with the provided id does not exist.");
            }

            user.Update(request);
            var returnUser = userRepository.UpdateUser(user);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            return returnUser;
        }

        public async Task<bool> UserExistAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            return await userRepository.ExistAsync(request.Email, request.PhoneNumber, cancellationToken);
        }

        public async Task<UserResponse> AssignRoleAsync(Guid roleId, Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User with the provided id does not exist.");
            }
            var role = await roleRepository.GetRoleByIdAsync(roleId, cancellationToken);
            if (role is null)
            {
                throw new NotFoundException($"Role with id: {roleId} cannot be found");
            }
            user.UpdateRole(roleId);
            var assignedRole = userRepository.UpdateUser(user);
            var changes = await unitofWork.SaveChanges(cancellationToken);
            if (changes <= 0)
            {
                throw new SaveOperationException("Failed to assign the role to the user. Please try again.");
            }
            return assignedRole;
        }
    }
}
