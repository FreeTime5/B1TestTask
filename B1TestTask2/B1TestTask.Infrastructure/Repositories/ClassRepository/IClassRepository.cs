using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.BaseRepository;

namespace B1TestTask2.Infrastructure.Repositories.ClassRepository
{
    public interface IClassRepository : IBaseRepository<Class>
    {
        public void ChangeTracking(IEnumerable<Class> classes);
    }
}
