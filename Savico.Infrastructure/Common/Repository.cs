namespace Savico.Infrastructure.Common
{
    using Microsoft.EntityFrameworkCore;

    public class Repository : IRepository
    {
        private readonly DbContext dbContext;

        public Repository(SavicoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> DbSet<T>() where T : class
        {
            return dbContext.Set<T>();
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllAsReadOnly<T>() where T : class
        {
            return DbSet<T>().AsNoTracking();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }
        public async Task RemoveAsync<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);
        }
        public async Task RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            DbSet<T>().RemoveRange(entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = dbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }

        public async Task<PagedResult<T>> GetPagedAsync<T>(
            int pageNumber,
            int pageSize,
            Func<IQueryable<T>, IQueryable<T>>? includes = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null) where T : class
        {
            var query = DbSet<T>().AsQueryable();

            if (filter != null)
            {
                query = filter(query);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

    }
}
