using CoopApplication.Domain.DTOs.RequestModels;
using CoopApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await userService.GetAllUsersAsync(cancellationToken);
            return Ok(users);
        }

        [HttpGet("{userId:guid}/user")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(userId, cancellationToken);
            return Ok(user);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByEmailAsync(email, cancellationToken);
            return Ok(user);
        }

        [HttpPost("create-member")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest request, CancellationToken cancellationToken)
        {
            var createdUser = await userService.CreateUserAsync(request, cancellationToken);
            return Created(nameof(GetUserById), createdUser);
        }

        [HttpPut("{userId:guid}/update")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var updatedUser = await userService.UpdateUserAsync(userId, request, cancellationToken);
            return Ok(updatedUser);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUserAsync([FromQuery] string searchTerm, [FromQuery] bool isActive = true, CancellationToken cancellationToken = default)
        {
            var search = new SearchUser(searchTerm, isActive);
            var users = await userService.SearchUserAsync(search, cancellationToken);
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var loginResponse = await userService.UserExistAsync(request, cancellationToken);
            return Ok(loginResponse);
        }

        [HttpPut("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromQuery] Guid userId, [FromQuery] Guid roleId, CancellationToken cancellationToken)
        {
            var response = await userService.AssignRoleAsync(userId, roleId, cancellationToken);
            return Ok(response);
        }
    }
}
