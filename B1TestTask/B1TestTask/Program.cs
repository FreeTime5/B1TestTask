using B1TestTask.Domain.Interfaces;
using B1TestTask.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;

IServiceCollection services = new ServiceCollection();
services.AddServices();

var provider = services.BuildServiceProvider();
var textService = provider.GetService<ITextService>() ?? throw new Exception();
var fileService = provider.GetService<IFileService>() ?? throw new Exception();

var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/files");

//Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/files");
//var folder = Directory.GetCurrentDirectory() + "/files";
//var taskList = new List<Task>();
//for (var i = 0; i < 100; i++)
//{
//    taskList.Add(Task.Run(() =>
//    {
//        using var stream = fileService.CreateFile(folder);

//        for (var j = 0; j < 100000; j++)
//        {
//            stream.WriteLine(textService.GenerateString());
//        }
//    }));
//}

//Task.WaitAll(taskList.ToArray());

Console.WriteLine(await fileService.UnionFiles("qoa")); 