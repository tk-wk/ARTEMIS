using ProjectARTEMIS.Infrastructure.Persistence;

public interface IPlayerProfileRepository : IRepository<PlayerProfile>
{
}

public class PlayerProfileRepository : Repository<PlayerProfile>, IPlayerProfileRepository
{
    public PlayerProfileRepository(MyDbContext context) : base(context)
    {
    }
}