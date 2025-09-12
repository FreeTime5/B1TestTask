using B1TestTask.Domain.Interfaces;
using B1TestTask.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace B1TestTask.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ITextService, TextService>();
            services.AddSingleton<ITaskService, TaskService>();
            return services;
        }
    }
}
