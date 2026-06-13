using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectARTEMIS.Controllers;

[Authorize] 
[ApiController]
[Route("api/v1/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "User identifier missing or invalid in token." });
        }

        try
        {
            var dashboardData = await _dashboardService.GetDashboardInfo(userId);
            return Ok(dashboardData);
        }
        catch (NullReferenceException)
        {
            return NotFound(new { message = "Dashboard records not found for this user." });
        }
    }
}

public record DashboardDto
{
    public string Status { get; set; } // Whitelisted if Accepted, Pending if Pending, Rejected if Rejected (appeal again)
    public DateTime TimeToday { get; set; } // basically used for the countdown. 

}
