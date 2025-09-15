namespace B1TestTask.Infrastructure.Interfaces
{
    public interface IImportFileProgressReporter
    {
        public void ReportProgress(int current, int total, string status);

        public void ReportComplete(int totalRecords, TimeSpan elapsedTime);

        public void ReportError(string errorMessage, int lineNumber);
    }
}
