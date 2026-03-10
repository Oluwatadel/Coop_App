using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.Entities;
using CoopApplication.Domain.Enums;
using CoopApplication.Persistence.Repository.Implementations;

namespace CoopApplication.Persistence.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction, CancellationToken cancellationToken);
        Task<Transaction?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Transaction>> GetTransactionsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Transaction>> GetAllTransactionsAsync(CancellationToken cancellationToken);
        Transaction UpdateTransaction(Transaction transaction);
        Task<IReadOnlyList<TransactionFilterDto>> FilterTransactionsAsync(Guid? UserId, DateTime? date,
            decimal? Amount,
            TransactionType? transactionType,
            PaymentMethod? paymentMethod, CancellationToken cancellationToken);
    }
}
