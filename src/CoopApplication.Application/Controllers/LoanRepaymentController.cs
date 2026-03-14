using CoopApplication.Domain.DTOs.ResponseModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly ILoanRepaymentService _loanRepaymentService;

        public LoanRepaymentController(ILoanRepaymentService loanRepaymentService)
        {
            _loanRepaymentService = loanRepaymentService;
        }

        [HttpGet("loan/{loanId}")]
        public async Task<ActionResult<IReadOnlyList<LoanRepaymentResponse>>> GetRepaymentsByLoanId(
            Guid loanId,
            CancellationToken cancellationToken)
        {
            var repayments = await _loanRepaymentService
                .GetRepaymentsByLoanIdAsync(loanId, cancellationToken);

            return Ok(repayments);
        }

        [HttpGet("{repaymentId}")]
        public async Task<ActionResult<LoanRepaymentResponse>> GetRepaymentById(
            Guid repaymentId,
            CancellationToken cancellationToken)
        {
            var repayment = await _loanRepaymentService
                .GetRepaymentByIdAsync(repaymentId, cancellationToken);

            if (repayment == null)
                return NotFound();

            return Ok(repayment);
        }
    }
}