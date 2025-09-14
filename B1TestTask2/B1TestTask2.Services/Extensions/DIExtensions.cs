using B1TestTask2.Infrastructure.Services;
using B1TestTask2.Services.Services.ExcelParser;
using B1TestTask2.Services.Services.FileLoader;
using B1TestTask2.Services.Services.FileLoader.Implementations;
using B1TestTask2.Services.Services.FileManager;
using B1TestTask2.Services.Services.FileManager.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace B1TestTask2.Services.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileLoader, FileLoader>();
            services.AddScoped<IExcelParser, ExcelParser>();
            services.AddScoped<IFileManager, FileManager>();
            return services;
        }
    }
}
