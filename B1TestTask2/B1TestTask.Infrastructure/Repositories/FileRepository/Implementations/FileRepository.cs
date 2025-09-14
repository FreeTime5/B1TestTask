using B1TestTask.Infrastructure;
using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.FileRepository.Implementations
{
    internal class FileRepository : BaseRepository<FileMetadata>, IFileRepository
    {
        public FileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<FileMetadata>> GetAllWithBanksAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Files.Include(f => f.Bank).ToListAsync(cancellationToken);
        }

        public async Task<FileMetadata> FirstOrDefaultWithBankAsync(Expression<Func<FileMetadata, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Files.Include(f => f.Bank).FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
