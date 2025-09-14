using B1TestTask.Infrastructure;
using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.RecordRepository.Implementations
{
    internal class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Record>> FindByWithClassAsync(Expression<Func<Record, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Records.Include(r => r.Class).Where(predicate).OrderBy(r => r.BankAccount).ToListAsync(cancellationToken);
        }
    }
}
