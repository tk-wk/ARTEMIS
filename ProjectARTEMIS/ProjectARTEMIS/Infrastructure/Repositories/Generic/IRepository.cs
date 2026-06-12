public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    IQueryable<T> GetAll();
    Task<T?> GetById(Guid id);
}
