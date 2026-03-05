using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class UnitOfWork(CoopDbContext context) : IUnitofWork
    {
        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
