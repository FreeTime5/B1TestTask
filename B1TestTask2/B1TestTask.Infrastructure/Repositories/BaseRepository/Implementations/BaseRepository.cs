using B1TestTask.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<T> set;

        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.set = dbContext.Set<T>();
        }

        public async Task AddAsync(T item, CancellationToken cancellationToken = default)
        {
            set.Add(item);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            dbContext.AddRange(items);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await set.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await set.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default)
        {
            return await set.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await set.ToListAsync(cancellationToken);
        }
    }
}
