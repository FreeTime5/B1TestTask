using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        public Task AddRangeAsync(IEnumerable<T> items, CancellationToken cancellationToken = default);

        public Task AddAsync(T Itme, CancellationToken cancellationToken = default);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        public Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<T>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken = default);
    }
}
