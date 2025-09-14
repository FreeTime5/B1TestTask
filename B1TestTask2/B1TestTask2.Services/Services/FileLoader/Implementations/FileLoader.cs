using B1TestTask2.Infrastructure.Repositories.ClassRepository;
using B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository;
using B1TestTask2.Infrastructure.Repositories.FileRepository;
using B1TestTask2.Infrastructure.Repositories.RecordRepository;
using B1TestTask2.Services.Exceptions;
using B1TestTask2.Services.Services.ExcelParser;

namespace B1TestTask2.Services.Services.FileLoader.Implementations
{
    internal class FileLoader : IFileLoader
    {
        private readonly IExcelParser excelParser;
        private readonly IRecordRepository recordRepository;
        private readonly IClassTotalsRepository classTotalsRepository;
        private readonly IFileRepository fileRepository;
        private readonly IClassRepository classRepository;

        public FileLoader(IExcelParser excelParser,
            IRecordRepository recordRepository,
            IClassTotalsRepository classTotalsRepository,
            IFileRepository fileRepository,
            IClassRepository classRepository)
        {
            this.excelParser = excelParser;
            this.recordRepository = recordRepository;
            this.classTotalsRepository = classTotalsRepository;
            this.fileRepository = fileRepository;
            this.classRepository = classRepository;
        }

        public async Task LoadFileAsync(string fileName, Stream readStream, CancellationToken cancellationToken = default)
        {
            var file = await fileRepository.FirstOrDefaultAsync(f => f.FileName == fileName, cancellationToken);
            if (file != null)
            {
                throw new ItemAlreadyExistsException("File with same name already exists");
            }

            var parsingResult = excelParser.ParseExecelFile(fileName, readStream);

            var existingClasses = await classRepository.GetAllAsNoTrackingAsync();
            var classes = parsingResult.ClassTotals
                .Where(t => !t.IsSubclass)
                .Select(t => t.Class);
            var extraClasses = classes
                .Where(c => !existingClasses
                .Any(ec => ec.Name == c.Name));

            if (extraClasses.Count() != 0)
            {
                await classRepository.AddRangeAsync(extraClasses);
            }
            else
            {
                var classesToTrack = classes.Where(c => !extraClasses.Any(ec => ec.Name == c.Name)).ToList();
                classRepository.ChangeTracking(classesToTrack);
            }
            await fileRepository.AddAsync(parsingResult.File, cancellationToken);
            await recordRepository.AddRangeAsync(parsingResult.Records, cancellationToken);
            await classTotalsRepository.AddRangeAsync(parsingResult.ClassTotals, cancellationToken);
        }
    }
}
