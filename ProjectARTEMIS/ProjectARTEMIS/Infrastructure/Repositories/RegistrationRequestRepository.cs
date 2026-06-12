using ProjectARTEMIS.Infrastructure.Persistence;

public interface IRegistrationRequestRepository : IRepository<RegistrationRequest>
{
}

public class RegistrationRequestRepository : Repository<RegistrationRequest>, IRegistrationRequestRepository
{
    public RegistrationRequestRepository(MyDbContext context) : base(context)
    {
    }
}