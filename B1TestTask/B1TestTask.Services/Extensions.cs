using B1TestTask.Domain.Interfaces;
using B1TestTask.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace B1TestTask.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var folder = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/files").FullName;
            services.AddSingleton<IFileService, FileService>(provider => new FileService(folder));
            services.AddSingleton<ITextService, TextService>();
            return services;
        }
    }
}
