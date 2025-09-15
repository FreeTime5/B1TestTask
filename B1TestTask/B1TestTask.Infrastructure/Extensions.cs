using B1TestTask.Data.Interfaces;
using B1TestTask.Infrastructure.Interfaces;
using B1TestTask.Infrastructure.Services.CommandExecutor;
using B1TestTask.Infrastructure.Services.CommandExecutor.Implementations;
using B1TestTask.Infrastructure.Services.FileImporter;
using B1TestTask.Infrastructure.Services.FileImporter.Implementations;
using B1TestTask.Infrastructure.Services.ProcedureExecutor;
using B1TestTask.Infrastructure.Services.ProcedureExecutor.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace B1TestTask.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            ILoggerFactory factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = factory.CreateLogger("Program");

            var connectinoString = Environment.GetEnvironmentVariable("ConnectionStrings__B1TestDatabase") ?? throw new Exception();
            services.AddScoped<IFileImporter, FileImporter>(provider =>
            {
                var progressReporter = provider.GetService<IImportFileProgressReporter>() ?? throw new Exception();
                return new FileImporter(connectinoString, progressReporter);
            });

            services.AddScoped<ICommandExecutor, CommandExecutor>(_ =>
            {
                return new CommandExecutor(connectinoString, logger);
            });

            services.AddScoped<IProcedureExecutor, ProcedureExecutor>(provider =>
            {
                var fileService = provider.GetService<IFileReader>() ?? throw new Exception();
                var commandExecutor = provider.GetService<ICommandExecutor>() ?? throw new Exception();
                return new ProcedureExecutor(connectinoString, fileService, commandExecutor);
            });

            return services;
        }
    }
}
