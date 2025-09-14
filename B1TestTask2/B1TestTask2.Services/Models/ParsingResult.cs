using B1TestTask2.Domain.Models;

namespace B1TestTask2.Services.Models
{
    public class ParsingResult
    {
        public FileMetadata File { get; set; }
        public List<Record> Records { get; set; }
        public List<ClassTotals> ClassTotals { get; set; }
    }
}
