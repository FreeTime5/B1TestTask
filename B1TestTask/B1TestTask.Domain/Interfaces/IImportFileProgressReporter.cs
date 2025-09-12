using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Interfaces
{
    public interface IImportFileProgressReporter
    {
        public void ReportProgress(int current, int total, string status);

        public void ReportComplete(int totalRecords, TimeSpan elapsedTime);

        public void ReportError(string errorMessage, int lineNumber);
    }
}
