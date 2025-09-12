using B1TestTask.Domain.Interfaces;
using B1TestTask.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var folder = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/files").FullName;
            ILoggerFactory factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger logger = factory.CreateLogger("Program");

            var connectinoString = Environment.GetEnvironmentVariable("ConnectionStrings__B1TestDatabase") ?? throw new Exception();
            services.AddSingleton<IImportFileProgressReporter, ConsoleImportFileProgressReporter>();
            services.AddSingleton<IFileImporter, FileImporter>(provider =>
            {
                var progressReporter = provider.GetService<IImportFileProgressReporter>() ?? throw new Exception();
                return new FileImporter(connectinoString, progressReporter);
            });

            services.AddSingleton<ICommandExecutor, CommandExecutor>(_ =>
            {
                return new CommandExecutor(connectinoString, logger);
            });

            services.AddSingleton<IProcedureExecutor, TaskProcedureExecutor>(provider =>
            {
                var fileService = provider.GetService<IFileService>() ?? throw new Exception();
                var commandExecutor = provider.GetService<ICommandExecutor>() ?? throw new Exception();
                return new TaskProcedureExecutor(connectinoString, fileService, commandExecutor);
            });
            services.AddSingleton<IFileService, FileService>(_ =>
            {
                return new FileService(folder, logger);
            });

            return services;
        }
    }
}
