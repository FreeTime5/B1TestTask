using B1TestTask2.Services.Models;

namespace B1TestTask2.Services.Services.ExcelParser
{
    public interface IExcelParser
    {
        public ParsingResult ParseExecelFile(string fileName, Stream readStream);
    }
}
