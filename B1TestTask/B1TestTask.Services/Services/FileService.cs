using B1TestTask.Domain.Interfaces;
using System.Drawing;
using System.Text;

namespace B1TestTask.Services.Services
{
    internal class FileService : IFileService
    {
        private readonly string folder;

        public FileService(string folder)
        {
            this.folder = folder;
        }

        public StreamWriter CreateFile()
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";

            return File.CreateText(folder + "/" + fileName);
        }

        public Task UnionFiles()
        {
            var inputFilePaths = Directory.GetFiles(folder, "*.txt");
            Console.WriteLine("Number of files: {0}", inputFilePaths.Length);
            Directory.CreateDirectory(folder + "/result");

            return Task.Run(() =>
            {
                using (var outputStream = File.Create(folder + "/result/union.txt"))
                {
                    foreach (var inputFilePath in inputFilePaths)
                    {
                        using (var inputStream = TextReader.Synchronized(File.OpenText(inputFilePath)))
                        {
                            var content = inputStream.ReadToEnd();
                            var bytes = Encoding.UTF8.GetBytes(content);
                            outputStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            });
        }

        public Task<int> UnionFiles(string subString)
        {
            var inputFilePaths = Directory.GetFiles(folder, "*.txt");
            Console.WriteLine("Number of files: {0}", inputFilePaths.Length);
            Directory.CreateDirectory(folder + "/result");

            return Task.Run(() =>
            {
                int counter = 0;
                using (var outputStream = File.Create(folder + "/result/union.txt"))
                {
                    foreach (var inputFilePath in inputFilePaths)
                    {
                        using (var inputStream = TextReader.Synchronized(File.OpenText(inputFilePath)))
                        {
                            string? line;
                            while ((line = inputStream.ReadLine()) != null)
                            {
                                if (line.Contains(subString))
                                {
                                    counter++;
                                    continue;
                                }
                                var bytes = Encoding.UTF8.GetBytes(line);
                                outputStream.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }
                }
                return counter;
            });
        }
    }
}
