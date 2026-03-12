using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/loans")]
    public class LoanTakenController : ControllerBase
    {
        private readonly ILoanTakenService _loanTakenService;

        public LoanTakenController(ILoanTakenService loanTakenService)
        {
            _loanTakenService = loanTakenService;
        }

        [HttpPost("request")]
        public async Task<ActionResult<LoanTakenResponse>> RequestLoan(
            [FromBody] LoanTakenRequest request,
            CancellationToken cancellationToken)
        {
            var loan = await _loanTakenService.RequestLoanAsync(request, cancellationToken);

            return Ok(loan);
        }

        [HttpPut("{loanId}/approve")]
        public async Task<ActionResult<LoanTakenResponse>> ApproveLoan(
            Guid loanId,
            [FromQuery] Guid approverId,
            CancellationToken cancellationToken)
        {
            var loan = await _loanTakenService.ApproveLoanAsync(loanId, approverId, cancellationToken);

            return Ok(loan);
        }

        [HttpGet("{loanId}")]
        public async Task<ActionResult<LoanTakenResponse>> GetLoanById(
            Guid loanId,
            CancellationToken cancellationToken)
        {
            var loan = await _loanTakenService.GetLoanByIdAsync(loanId, cancellationToken);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IReadOnlyList<LoanTakenResponse>>> GetLoansByUser(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var loans = await _loanTakenService.GetLoansByUserIdAsync(userId, cancellationToken);

            return Ok(loans);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LoanTakenResponse>>> GetAllLoans(
            CancellationToken cancellationToken)
        {
            var loans = await _loanTakenService.GetAllLoansAsync(cancellationToken);

            return Ok(loans);
        }
    }
}