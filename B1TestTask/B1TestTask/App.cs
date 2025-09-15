using B1TestTask.Services.Services.TaskService;
using System.Net.WebSockets;

namespace B1TestTask
{
    internal class App
    {
        private const int FILESCOUNT = 100;
        private readonly ITaskService taskService;

        public App(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        private async Task DisplayMenu()
        {
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine("1) Union files into one");
            Console.WriteLine("2) Import files into database");
            Console.WriteLine("3) Find sum of integers and median of decimals");
            Console.WriteLine("0) Exit");

            await CheckMenuInput();
        }

        private async Task CheckMenuInput()
        {
            var input = Console.ReadKey(true).KeyChar;

            switch (input)
            {
                case '1':
                    await UnionFiles();
                    break;
                case '2':
                    await ImportFiles();
                    break;
                case '3':
                    await ExecuteProcedure();
                    break;
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    break;

            }
        }

        private async Task UnionFiles()
        {
            Console.Clear();
            Console.WriteLine("Input substring: ");
            var subString = Console.ReadLine();
            var deletedLines = await taskService.UnionFiles(subString);
            Console.WriteLine($"Lines were ignored: {deletedLines}");
        }

        private async Task ImportFiles()
        {
            Console.Clear();
            Console.WriteLine("Input file number:");
            var fileIndex = Console.ReadLine();

            if (!int.TryParse(fileIndex, out int index) || index < 0 || index > 99)
            {
                Console.WriteLine("Incorrect input");
            }
            else
            {
                var files = taskService.GetAllFiles();
                var filePath = files.ElementAt(index);
                await taskService.ImportFiles(filePath);
            }

        }

        private async Task ExecuteProcedure()
        {
            Console.Clear();

            var results = await taskService.GetSumAndMedian();

            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key} - {result.Value}");
            }
        }

        private async Task InitializeApp()
        {
            var files = taskService.GetAllFiles();
            var lackOfFiles = FILESCOUNT - files.Count();
            for (var i = 0; i < lackOfFiles; i++)
            {
                await taskService.CreateFile();
            }
        }

        public async Task Run()
        {
            while (true)
            {
                await InitializeApp();
                await DisplayMenu();
            }
        }
    }
}
