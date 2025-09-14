namespace B1TestTask2.Services.Dtos.Response
{
    public class FileMetadataDto
    {
        public int FileId { get; set; }

        public string FileName { get; set; }

        public string ReportTitle { get; set; }

        public string Currency { get; set; }

        public string BankName { get; set; }

        public string PeriodStart { get; set; }

        public string PeriodEnd { get; set; }

        public string UploadDate { get; set; }
    }
}
