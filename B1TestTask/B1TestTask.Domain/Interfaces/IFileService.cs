using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Interfaces
{
    public interface IFileService
    {
        public StreamWriter CreateFile();

        public Task UnionFiles();

        public Task<int> UnionFiles(string subString);
    }
}
