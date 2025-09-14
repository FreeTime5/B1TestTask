using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository
{
    public interface IClassTotalsRepository : IBaseRepository<ClassTotals>
    {
        public Task<IEnumerable<ClassTotals>> FindByWithClassAsync(Expression<Func<ClassTotals, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
