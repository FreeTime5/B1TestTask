using B1TestTask.Infrastructure;
using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations;

namespace B1TestTask2.Infrastructure.Repositories.ClassRepository.Implementations
{
    internal class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public void ChangeTracking(IEnumerable<Class> classes)
        {
            dbContext.Classes.AttachRange(classes);
        }
    }
}
