using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;

namespace CoopApplication.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> ProcessTransactionAsync(TransactionRequestDto request,CancellationToken cancellationToken);

        Task<TransactionResponse?> GetTransactionByIdAsync(Guid transactionId, CancellationToken cancellationToken);

        Task<IReadOnlyList<TransactionResponse>> GetTransactionsByUserIdAsync(Guid userId,CancellationToken cancellationToken);

        Task<IReadOnlyList<TransactionResponse>> GetAllTransactionsAsync(CancellationToken cancellationToken);
       
    }
}
