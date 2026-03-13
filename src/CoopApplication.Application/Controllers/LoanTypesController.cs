using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [Route("api/v{version:apiVersion}/loan-repayments")]
    [ApiController]
    public class LoanRepaymentsController(ILoanRepaymentService loanRepaymentService) : ControllerBase
    {
        [HttpGet("{repaymentId:guid}")]
        public async Task<IActionResult> GetLoanRepayment(
            [FromRoute] Guid repaymentId,
            CancellationToken cancellationToken)
        {
            var response = await loanRepaymentService
                .GetRepaymentByIdAsync(repaymentId, cancellationToken);

            if (response is null)
                return NotFound();

            return Ok(response);
        }

        [HttpGet("loan/{loanId:guid}")]
        public async Task<IActionResult> GetLoanRepaymentsByLoanId(
            [FromRoute] Guid loanId,
            CancellationToken cancellationToken)
        {
            var responses = await loanRepaymentService
                .GetRepaymentsByLoanIdAsync(loanId, cancellationToken);

            return Ok(responses);
        }
    }
}