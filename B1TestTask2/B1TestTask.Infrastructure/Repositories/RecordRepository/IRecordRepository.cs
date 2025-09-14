using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.RecordRepository
{
    public interface IRecordRepository : IBaseRepository<Record>
    {
        public Task<IEnumerable<Record>> FindByWithClassAsync(Expression<Func<Record, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
