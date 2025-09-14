using B1TestTask.Infrastructure;
using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository.Implementations
{
    internal class ClassTotalsRepository : BaseRepository<ClassTotals>, IClassTotalsRepository
    {
        public ClassTotalsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ClassTotals>> FindByWithClassAsync(Expression<Func<ClassTotals, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.ClassTotals.Include(ct => ct.Class).Where(predicate).OrderBy(ct => ct.ClassId).ToListAsync(cancellationToken);
        }
    }
}
