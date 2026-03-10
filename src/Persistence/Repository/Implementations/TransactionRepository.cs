using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoopApplication.Persistence.Repository.Implementations
{
    public class TransactionRepository(CoopDbContext context) : ITransactionRepository
    {
        public async Task<Transaction> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
        {
            await context.Transactions.AddAsync(transaction, cancellationToken);
            return transaction;
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken)
        {
            return await context.Transactions
                .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);
        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken)
        {
            return await context.Transactions.ToListAsync(cancellationToken);
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            return transaction;
        }

        public async Task<IReadOnlyList<TransactionFilterDto>> FilterTransactionsAsync(Guid? UserId, DateTime? date, 
            decimal? Amount,
            TransactionType? transactionType,
            PaymentMethod? paymentMethod, CancellationToken cancellationToken)
        {
            var query = from tran in context.Transactions
                        join user in context.Users on tran.UserId equals user.Id
                        join assoc in context.Associations on user.AssociationId equals assoc.Id
                        select new { tran, user, assoc };
            if (UserId.HasValue)
            {
                query = query.Where(t => t.tran.UserId == UserId.Value);
            }
            if(date.HasValue)
            {
                query = query.Where(t => t.tran.Date == date.Value.Date);
            }
            if(Amount.HasValue)
            {
                query = query.Where(t => t.tran.Amount <= Amount.Value);
            }
            if(transactionType.HasValue)
            {
                query = query.Where(t => t.tran.TransactionType == transactionType);
            }
            if(paymentMethod.HasValue)
            {
                query = query.Where(t => t.tran.PaymentMethod == paymentMethod);
            }

            var data = await query
                .Select(a => new TransactionFilterDto
                {
                    FullName = $"{a.user.LastName} {a.user.FirstName}",
                    AssociationName = a.assoc.Name,
                    Date = a.tran.Date,
                    Amount = a.tran.Amount,
                    TransactionType = a.tran.TransactionType,
                    PaymentMethod = a.tran.PaymentMethod
                })
                .ToListAsync(cancellationToken);
            return data;
        }
    }
}
