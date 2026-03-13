using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [Route("api/v{version:apiVersion}/loan-types")]
    [ApiController]
    public class LoanTypesController(ILoanTypeService loanTypeService) : ControllerBase
    {
        [HttpGet("{loanTypeId:guid}/loan-type")]
        public async Task<IActionResult> GetLoanType([FromRoute] Guid loanTypeId, CancellationToken cancellationToken)
        {
            var response = await loanTypeService.GetLoanTypeAsync(loanTypeId, cancellationToken);
            return Ok(response);
        }
        
        [HttpGet("loan-type")]
        public async Task<IActionResult> GetLoanTypeByName([FromQuery] string loanTypeName, CancellationToken cancellationToken)
        {
            var response = await loanTypeService.GetLoanTypeByNameAsync(loanTypeName, cancellationToken);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLoanType([FromBody] CreateLoanTypeRequest request,
            CancellationToken cancellationToken)
        {
            var reaponse = await loanTypeService.CreateLoanTypeAsync(request, cancellationToken);
            return Created(nameof(request), reaponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoanTypes(CancellationToken cancellationToken)
        {
            var responses = await loanTypeService.GetAllLoanTpesAsync(cancellationToken);
            return Ok(responses);
        }

        [HttpPatch("{loanTypeId:guid}/update")]
        public async Task<IActionResult> UpdateLoanTypes([FromRoute] Guid loanTypeId, 
            [FromBody] UpdateLoanTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await loanTypeService.UpdateTypeAsync(loanTypeId, request, cancellationToken);
            return Ok(result);
        }
    }
}
