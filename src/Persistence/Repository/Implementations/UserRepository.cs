using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class UserRepository(CoopDbContext context) : IUserRepository
    {
        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user, cancellationToken);
            return user;
        }

        public async Task<bool> ExistAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Users
                .AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await context.Users
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await context.Users
                .FirstOrDefaultAsync( u => u.Id == userId, cancellationToken);
        }

        public User UpdateUser(User user)
        {
            context.Users.Update(user);
            return user;
        }
    }
}
