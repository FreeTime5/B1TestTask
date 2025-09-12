using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Interfaces
{
    public interface ITaskService
    {
        public Task ImportFiles(string filePath);

        public Task<int> UnionFiles(string subString = "");

        public Task<Dictionary<string, object>> GetSumAndMedian();

        public IEnumerable<string> GetAllFiles();

        public Task CreateFile();
    }
}
