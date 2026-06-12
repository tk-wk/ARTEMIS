using Microsoft.EntityFrameworkCore;
using ProjectARTEMIS.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MyDbContext _context;
    protected readonly DbSet<T> _set;
    public Repository(MyDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public void Add(T entity) => _set.Add(entity);

    public void Delete(T entity) => _set.Remove(entity);

    public IQueryable<T> GetAll() => _set.AsQueryable();

    public async Task<T?> GetById(Guid id) => await _set.FindAsync(id);

    public void Update(T entity) => _set.Update(entity);
}
