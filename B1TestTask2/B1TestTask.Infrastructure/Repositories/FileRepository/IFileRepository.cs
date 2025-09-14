using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository;
using System.Linq.Expressions;

namespace B1TestTask2.Infrastructure.Repositories.FileRepository
{
    public interface IFileRepository : IBaseRepository<FileMetadata>
    {
        public Task<IEnumerable<FileMetadata>> GetAllWithBanksAsync(CancellationToken cancellationToken = default);

        public Task<FileMetadata> FirstOrDefaultWithBankAsync(Expression<Func<FileMetadata, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
