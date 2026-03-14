using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoopApplication.api.Controllers
{
    [Route("api/v{version:apiVersion}/dashboards")]
    [ApiController]
    public class DashBoardsController(IDashBoardService dashBoardServices) : ControllerBase
    {
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetUserDashboardOverview([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var dashboardDetails = await dashBoardServices.GetUserDashBoardOverview(userId, cancellationToken);
            return Ok(dashboardDetails);
        }
        
        [HttpGet("admin/{userId:guid}")]
        public async Task<IActionResult> GetAdminDashboardOverview([FromRoute] Guid userId, CancellationToken cancellationToken)
            {
            var dashboardDetails = await dashBoardServices.GetAdminDashBoardOverview(userId, cancellationToken);
            return Ok(dashboardDetails);
        }
        
        [HttpGet("manager/{userId:guid}")]
        public async Task<IActionResult> GetManagerDashboardOverview([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var dashboardDetails = await dashBoardServices.GetManagerDashBoardOverview(userId, cancellationToken);
            return Ok(dashboardDetails);
        }

    }
}
