using B1TestTask;
using B1TestTask.Infrastructure;
using B1TestTask.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

IServiceCollection services = new ServiceCollection();


services.AddSingleton<App>();
services.AddServices();
services.AddInfrastructureServices();

var provider = services.BuildServiceProvider();

var app = provider.GetService<App>() ?? throw new Exception();

await app.Run();


