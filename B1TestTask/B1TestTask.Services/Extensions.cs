using B1TestTask.Data.Interfaces;
using B1TestTask.Infrastructure.Interfaces;
using B1TestTask.Services.Services.FileService;
using B1TestTask.Services.Services.FileService.Implementations;
using B1TestTask.Services.Services.IportFileProgressReporter;
using B1TestTask.Services.Services.TaskService;
using B1TestTask.Services.Services.TaskService.Implementations;
using B1TestTask.Services.Services.TextService;
using B1TestTask.Services.Services.TextService.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace B1TestTask.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var folder = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/files").FullName;
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IFileReader, FileService>(FileServiceImplementationFactory(folder));
            services.AddScoped<IFileService, FileService>(FileServiceImplementationFactory(folder));
            services.AddScoped<IImportFileProgressReporter, ImportFileProgressReporter>();
            return services;
        }

        private static Func<IServiceProvider, FileService> FileServiceImplementationFactory(string folder)
        {
            return (provider =>
            {
                ILogger logger = provider.GetService<ILogger>();
                return new FileService(folder, logger);
            });
        }
    }
}
