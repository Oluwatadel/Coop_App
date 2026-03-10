using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using CoopApplication.api.Exceptions;
using System.Data;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class UserRepository(CoopDbContext context) : IUserRepository
    {
        public async Task<UserResponse> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            var query = from u in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new {role, association};
            var result = await query.FirstOrDefaultAsync(u => u.association.Id == user.AssociationId && u.role.Id == user.RoleId, cancellationToken);
            return new UserResponse
            {
                UserId = user.Id,
                AssociationName = result!.association.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = result.role.Name
            };
        }

        public async Task<bool> ExistAsync(string email, string? phoneNumber, CancellationToken cancellationToken)
        {
            var query = context.Users.AsQueryable();
            if(!string.IsNullOrWhiteSpace(phoneNumber))
            {
                query = query.Where(u => u.Phone == phoneNumber);
            }
            if(!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email == email);
            }
            return await query
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<IReadOnlyList<UserResponse>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new UserResponse
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Role = role.Name
                        };
            return await query.OrderByDescending(u => u.LastName)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select user;
            return await query.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select user;
            return await query
                .FirstOrDefaultAsync( u => u.Id == userId, cancellationToken);
        }

        public UserResponse UpdateUser(User user)
        {
            context.Users.Update(user);
            var changes = context.SaveChanges();
            var association = context.Associations.FirstOrDefault(a => a.Id == user.AssociationId);
            var role = context.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            return new UserResponse
            {
                UserId = user.Id,
                AssociationName = association.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = role.Name
            };
        }

        public async Task<IReadOnlyList<UserResponse?>> SearchUserAsync(SearchUser request, CancellationToken cancellationToken)
        {
            //To add pagination later
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new { user, association, role };
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(u =>
                    (EF.Functions.ILike(u.user.FirstName, request.SearchTerm)
                    || EF.Functions.ILike(u.user.LastName, request.SearchTerm)
                    || EF.Functions.ILike(u.user.Email, request.SearchTerm)
                    || EF.Functions.ILike(u.user.Phone, request.SearchTerm))
                    && u.user.IsActive == request.IsActive
                );
            }

            var data = await query.OrderByDescending(u => u.user.CreatedAt)
                .ToListAsync(cancellationToken);
            return [..data.Select(a => new UserResponse
            {
                UserId = a.user.Id,
                AssociationName = a.association.Name,
                Email = a.user.Email,
                FirstName = a.user.FirstName,
                LastName = a.user.LastName,
                Phone = a.user.Phone,
                Role = a.role.Name
            })];
        }

        public async Task<IReadOnlyList<UserResponse>> GetMembersOfAnAssociation(Guid associationId, CancellationToken cancellationToken)
        {
            //To add pagination later
            var query = from user in context.Users
                        join association in context.Associations
                        on user.AssociationId equals association.Id
                        join role in context.Roles
                        on user.RoleId equals role.Id
                        select new UserResponse
                        {
                            UserId = user.Id,
                            AssociationName = association.Name,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Phone = user.Phone,
                            Role = role.Name
                        };
            var data = await query.OrderByDescending(u => u.LastName)
                .ToListAsync(cancellationToken);
            return data;
        }
    }

}
