namespace Savico.Infrastructure.Repositories.Contracts
{
    using System.Linq.Expressions;

    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        //IQueryable<T> All<T>() where T : class;
        //IQueryable<T> AllAsReadOnly<T>() where T : class;
        //Task AddAsync<T>(T entity) where T : class;
        //Task<int> SaveChangesAsync();
        //Task<T?> GetByIdAsync<T>(object id) where T : class;
        //Task RemoveAsync<T>(T entity) where T : class;
        //Task RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class;
        //void Detach<TEntity>(TEntity entity) where TEntity : class;
        //Task<PagedResult<T>> GetPagedAsync<T>(
        //int pageNumber,
        //int pageSize,
        //Func<IQueryable<T>, IQueryable<T>>? includes = null,
        //Func<IQueryable<T>, IQueryable<T>>? filter = null) where T : class;
    }
}
