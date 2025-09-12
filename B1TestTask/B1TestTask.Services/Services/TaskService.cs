using B1TestTask.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Services.Services
{
    internal class TaskService : ITaskService
    {
        private const string TABLENAME = "Records";
        private const int NUMBEROFSTRINGSINFILE = 100000;
        private readonly IFileImporter fileImporter;
        private readonly IFileService fileService;
        private readonly IProcedureExecutor procedureExecutor;
        private readonly ITextService textService;

        public TaskService(IFileImporter fileImporter,
            IFileService fileService,
            IProcedureExecutor procedureExecutor,
            ITextService textService)
        {
            this.fileImporter = fileImporter;
            this.fileService = fileService;
            this.procedureExecutor = procedureExecutor;
            this.textService = textService;
        }

        public async Task<Dictionary<string, object>> GetSumAndMedian()
        {
            if (!await procedureExecutor.ProcedureExists())
            {
                await procedureExecutor.CreateProcedure();
            } 
            var result = await procedureExecutor.ExecuteProcedure();

            return result;
        }

        public async Task ImportFiles(string filePath)
        {
            await fileImporter.ImportFileAsync(filePath, TABLENAME);
        }

        public IEnumerable<string> GetAllFiles()
        {
            return fileService.GetAllFiles();
        }

        public async Task<int> UnionFiles(string subString = "")
        {
            if (string.IsNullOrEmpty(subString))
            {
                await fileService.UnionFiles();
                return 0;
            }
            else
            {
                return await fileService.UnionFiles(subString);
            }
        }

        public async Task CreateFile()
        {
            using (var reader = fileService.CreateFile())
            {
                for (var i = 0; i < NUMBEROFSTRINGSINFILE; i++)
                {
                    var str = textService.GenerateString();
                    reader.WriteLine(str);
                }
            }
        }
    }
}
