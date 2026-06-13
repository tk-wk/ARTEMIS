using Microsoft.EntityFrameworkCore;
using ProjectARTEMIS.Infrastructure.Persistence;

public interface IPlayerProfileRepository : IRepository<PlayerProfile>
{
    Task<PlayerProfile?> GetByUserId(Guid userId);
}

public class PlayerProfileRepository : Repository<PlayerProfile>, IPlayerProfileRepository
{
    public PlayerProfileRepository(MyDbContext context) : base(context)
    {
    }
    public async Task<PlayerProfile?> GetByUserId(Guid userId) => _set
        .Include(x=>x.OnlineStatuses)
        .Include(x=>x.ProfileStatuses)
        .Include(x=>x.LinkedSocials)
        .FirstOrDefault(x => x.UserId == userId);
}