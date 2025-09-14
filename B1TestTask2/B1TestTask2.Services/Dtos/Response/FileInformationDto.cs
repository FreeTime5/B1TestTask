namespace B1TestTask2.Services.Dtos.Response
{
    public class FileInformationDto
    {
        public FileMetadataDto File { get; set; }
        public IEnumerable<RecordDto> Records { get; set; }
        public IEnumerable<ClassTotalsDto> ClassTotals { get; set; }
    }
}
