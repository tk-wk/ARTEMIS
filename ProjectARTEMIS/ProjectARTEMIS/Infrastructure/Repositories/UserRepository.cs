using ProjectARTEMIS.Infrastructure.Persistence;

public interface IUserRepository : IRepository<User>
{
    // Add custom queries here later if needed (e.g., Task<User?> GetByUsernameAsync(string username);)
}

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MyDbContext context) : base(context)
    {
    }
}