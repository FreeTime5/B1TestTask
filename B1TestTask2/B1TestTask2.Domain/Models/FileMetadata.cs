namespace B1TestTask2.Domain.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public Bank Bank { get; set; }
        public int BankId { get; set; }

        public string ReportTitle { get; set; }

        public DateTime PeriodStart { get; set; }

        public DateTime PeriodEnd { get; set; }

        public string Currency { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
