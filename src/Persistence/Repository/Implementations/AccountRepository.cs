using CoopApplication.Domain.Entities;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CoopApplication.Persistence.Repository.Implementations
{
    public class AccountRepository(CoopDbContext context) : IAccountRepository
    {
        public async Task<Account> CreateAccountAsync(Account account, CancellationToken cancellationToken)
        {
            await context.Accounts.AddAsync(account, cancellationToken);
            return account;
        }

        public async Task<Account?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken)
        {
            return await context.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);
        }

        public async Task<Account?> GetAccountByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await context.Accounts
                .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
        }

        public async Task<bool> ExistAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await context.Accounts
                .AnyAsync(a => a.UserId == userId, cancellationToken);
        }

        public async Task<IReadOnlyList<Account>> GetAllAccountAsync(CancellationToken cancellationToken)
        {
            return await context.Accounts.ToListAsync(cancellationToken);
        }

        public Account UpdateAccount(Account account)
        {
            context.Accounts.Update(account);
            return account;
        }
    }
}
