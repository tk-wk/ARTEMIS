using Microsoft.EntityFrameworkCore;
using ProjectARTEMIS.Infrastructure.Persistence;

public interface IWhitelistRequestRepository : IRepository<WhitelistRequest>
{
    Task<WhitelistRequest?> GetByUserId(Guid id);
}

public class WhitelistRequestRepository : Repository<WhitelistRequest>, IWhitelistRequestRepository
{
    public WhitelistRequestRepository(MyDbContext context) : base(context)
    {
    }

    public override async Task<WhitelistRequest?> GetById(Guid id)
    {
        return await _set
            .Include(r => r.Statuses)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public override IQueryable<WhitelistRequest> GetAll()
    {
        return _set
            .Include(r => r.Statuses);
    }

    public async Task<WhitelistRequest?> GetByUserId(Guid id) => await _set.Include(x => x.Statuses).FirstOrDefaultAsync(x => x.UserId == id);

}