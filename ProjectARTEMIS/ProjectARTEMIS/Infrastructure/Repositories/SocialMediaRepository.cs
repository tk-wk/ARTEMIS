using ProjectARTEMIS.Infrastructure.Persistence;

public interface ISocialMediaRepository : IRepository<SocialMedia>
{
}

public class SocialMediaRepository : Repository<SocialMedia>, ISocialMediaRepository
{
    public SocialMediaRepository(MyDbContext context) : base(context)
    {
    }
}