
using Microsoft.EntityFrameworkCore;

public class DashboardService
{
    private readonly IUnitOfWork _uow;

    public DashboardService(IUnitOfWork uow) => _uow = uow;

    public async Task<DashboardDto> GetDashboardInfo(Guid userId)
    {
        var profile = await _uow.PlayerProfiles.GetByUserId(userId);
        var whitelistRequest = await _uow.WhitelistRequests.GetByUserId(userId);
        var status = whitelistRequest?.CurrentStatus?.Status.ToString() ?? "None";

        return new DashboardDto
        {
            Status = status,
            TimeToday = DateTime.UtcNow,
        };
    }
}


