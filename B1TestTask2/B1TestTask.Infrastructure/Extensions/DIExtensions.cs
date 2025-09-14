using B1TestTask2.Infrastructure.Repositories.BaseRepository;
using B1TestTask2.Infrastructure.Repositories.BaseRepository.Implementations;
using B1TestTask2.Infrastructure.Repositories.ClassRepository;
using B1TestTask2.Infrastructure.Repositories.ClassRepository.Implementations;
using B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository;
using B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository.Implementations;
using B1TestTask2.Infrastructure.Repositories.FileRepository;
using B1TestTask2.Infrastructure.Repositories.FileRepository.Implementations;
using B1TestTask2.Infrastructure.Repositories.RecordRepository;
using B1TestTask2.Infrastructure.Repositories.RecordRepository.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B1TestTask.Infrastructure.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("B1TestDatabase");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddRepositories();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassTotalsRepository, ClassTotalsRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IRecordRepository, RecordRepository>();
            return services;
        }
    }
}
