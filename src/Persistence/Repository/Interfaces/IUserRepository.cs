using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.Entities;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface  IUserRepository
    {
        Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistAsync(string email, CancellationToken cancellationToken);
        Task<IReadOnlyList<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        User UpdateUser(User user);
    }
}
