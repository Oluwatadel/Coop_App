using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.Api.Controllers
{
    [Route("api/v{version:apiVersion}/transactions")]
    [ApiController]
    public class TransactionController(ITransactionService transactionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions(CancellationToken cancellationToken)
        {
            var transactions = await transactionService
                .GetAllTransactionsAsync(cancellationToken);

            return Ok(transactions);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
        {   
            var transaction = await transactionService
                .GetTransactionByIdAsync(id, cancellationToken);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetTransactionsByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var transactions = await transactionService
                .GetTransactionsByUserIdAsync(userId, cancellationToken);

            return Ok(transactions);
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessTransaction(
            [FromBody] TransactionRequestDto request,
            CancellationToken cancellationToken)
        {
            var transaction = await transactionService
                .ProcessTransactionAsync(request, cancellationToken);

            return Ok(transaction);
        }
    }
}