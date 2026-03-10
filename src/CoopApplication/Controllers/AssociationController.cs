using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [Route("api/v{version:apiVersion}/associations")]
    [ApiController]
    public class AssociationController(IAssociationService associationService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAssociations(CancellationToken cancellationToken)
        {
            var associations = await associationService.GetAllAssociation(cancellationToken);
            return Ok(associations);
        }

        [HttpGet]
        [Route("{id:guid}/association")]
        public async Task<IActionResult> GetAssociationById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var association = await associationService.GetAssociationByIdAsync(id, cancellationToken);
            return Ok(association);
        }

        [HttpGet("association")]
        public async Task<IActionResult> GetAssociationByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            var association = await associationService.GetAssociationByNameAsync(name, cancellationToken);
            return Ok(association);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAssociation([FromRoute] Guid id, [FromBody] string name, CancellationToken cancellationToken)
        {
            var updatedAssociation = await associationService.UpdateAssociationAsync(id, name, cancellationToken);
            return Ok(updatedAssociation);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAssociation([FromBody] CreateAssociationRequest request, CancellationToken cancellationToken)
        {
            var createdAssociation = await associationService.CreateAssociationAsync(request.AssociationName, request.Description, cancellationToken);
            return CreatedAtAction(nameof(GetAssociationById), createdAssociation);
        }
    }
}
