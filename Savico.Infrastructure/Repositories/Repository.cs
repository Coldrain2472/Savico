namespace Savico.Infrastructure.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Savico.Infrastructure.Repositories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repository(SavicoDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) // adds a new entity and saves changes
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity) // deletes an entity and saves changes
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) // takes an expression to filter records
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() // retrieves all entities from the database
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id) // finds a single entity by its Id.
        {
            return await dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity) // updates an existing entity and saves changes
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
        }


        //public void Detach<TEntity>(TEntity entity) where TEntity : class
        //{
        //    var entry = dbContext.Entry(entity);
        //    if (entry.State != EntityState.Detached)
        //    {
        //        entry.State = EntityState.Detached;
        //    }
        //}

        //public async Task<PagedResult<T>> GetPagedAsync<T>(
        //    int pageNumber,
        //    int pageSize,
        //    Func<IQueryable<T>, IQueryable<T>>? includes = null,
        //    Func<IQueryable<T>, IQueryable<T>>? filter = null) where T : class
        //{
        //    var query = DbSet<T>().AsQueryable();

        //    if (filter != null)
        //    {
        //        query = filter(query);
        //    }

        //    if (includes != null)
        //    {
        //        query = includes(query);
        //    }

        //    var totalCount = await query.CountAsync();

        //    var items = await query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return new PagedResult<T>
        //    {
        //        Items = items,
        //        TotalCount = totalCount
        //    };
        //}

    }
}
