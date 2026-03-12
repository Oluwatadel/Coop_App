using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/roles")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateRoleRequest request, CancellationToken cancellationToken = default)
        {
            var roleResponse = await roleService.CreateRoleAsync(request, cancellationToken);
            return Created("", roleResponse);
        }

        [HttpGet("{roleId}/role")]
        public async Task<IActionResult> GetRoleById([FromRoute] Guid roleId, CancellationToken cancellationToken = default)
        {
            var role = await roleService.GetRoleByIdAsync(roleId, cancellationToken);
            return Ok(role);
        }

        [HttpGet("role")]
        public async Task<IActionResult> GetRoleById([FromQuery] string roleName, CancellationToken cancellationToken = default)
        {
            var role = await roleService.GetRoleByNameAsync(roleName, cancellationToken);
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken = default)
        {
            var roles = await roleService.GetAllRolesAsync(cancellationToken);
            return Ok(roles);
        }
    }
}
