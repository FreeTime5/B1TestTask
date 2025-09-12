using B1TestTask.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Infrastructure.Services
{
    internal class FileService : IFileService
    {
        private readonly string folder;
        private readonly ILogger logger;

        public FileService(string folder, ILogger logger)
        {
            this.folder = folder;
            this.logger = logger;
        }

        public StreamWriter CreateFile()
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var fullName = folder + "/" + fileName;//
            return File.CreateText(fullName);
        }

        public async Task<string?> ReadFile(string filePath)
        {
            string? content = null;
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    content = await reader.ReadToEndAsync();
                }
            }
            catch (FileNotFoundException ex)
            {
                logger.LogError(ex.Message);
            }

            return content;
        }

        public async Task UnionFiles()
        {
            var inputFilePaths = Directory.GetFiles(folder, "*.txt");
            Directory.CreateDirectory(folder + "/result");//


            using (var outputStream = File.Create(folder + "/result/union.txt"))
            {
                //
                foreach (var inputFilePath in inputFilePaths)
                {
                    //
                    using (var inputStream = TextReader.Synchronized(File.OpenText(inputFilePath)))
                    {
                        var content = await inputStream.ReadToEndAsync();
                        var bytes = Encoding.UTF8.GetBytes(content);
                        outputStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public async Task<int> UnionFiles(string subString)
        {
            var inputFilePaths = Directory.GetFiles(folder, "*.txt");
            Directory.CreateDirectory(folder + "/result");

            int counter = 0;
            using (var outputStream = File.Create(folder + "/result/union.txt"))//
            {
                foreach (var inputFilePath in inputFilePaths)
                {
                    using (var inputStream = TextReader.Synchronized(File.OpenText(inputFilePath)))
                    {
                        string? line;
                        //
                        while ((line = await inputStream.ReadLineAsync()) != null)
                        {
                            if (line.Contains(subString))
                            {
                                counter++;
                            }
                            else
                            {
                                var bytes = Encoding.UTF8.GetBytes(line);
                                outputStream.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }
                }
            }

            return counter;
                
        }

        public IEnumerable<string> GetAllFiles()
        {
            var files = Directory.GetFiles(folder);
            return files;
        }
    }
}
