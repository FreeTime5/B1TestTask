using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Interfaces
{
    public interface IFileImporter
    {
        public Task ImportFileAsync(string filePath, string tableName);
    }
}
