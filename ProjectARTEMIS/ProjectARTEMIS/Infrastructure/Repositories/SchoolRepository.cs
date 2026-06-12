
using ProjectARTEMIS.Infrastructure.Persistence;

public interface ISchoolRepository : IRepository<School>
{

}

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    public SchoolRepository(MyDbContext context) : base(context)
    {
         
    }
}

