using CoopApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken);

        Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken);

        Task<Account?> GetAccountByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<bool> ExistAsync(Guid userId, CancellationToken cancellationToken);

        Task<IReadOnlyList<Account>> GetAllAccountAsync(CancellationToken cancellationToken);

        Account UpdateAccount(Account account);

    }
}
