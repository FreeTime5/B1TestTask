using B1TestTask2.Domain.Models;
using B1TestTask2.Infrastructure.Repositories.ClassTotalsRepository;
using B1TestTask2.Infrastructure.Repositories.FileRepository;
using B1TestTask2.Infrastructure.Repositories.RecordRepository;
using B1TestTask2.Services.Dtos.Response;
using B1TestTask2.Services.Exceptions;

namespace B1TestTask2.Services.Services.FileManager.Implementations
{
    internal class FileManager : IFileManager
    {
        private const string DateTimeFormat = "dd.MM.yyyy";
        private readonly IFileRepository fileRepository;
        private readonly IRecordRepository recordRepository;
        private readonly IClassTotalsRepository classTotalsRepository;

        public FileManager(IFileRepository fileRepository,
            IRecordRepository recordRepository,
            IClassTotalsRepository classTotalsRepository)
        {
            this.fileRepository = fileRepository;
            this.recordRepository = recordRepository;
            this.classTotalsRepository = classTotalsRepository;
        }

        public async Task<IEnumerable<FileMetadataDto>> GetAllFilesAsync(CancellationToken cancellationToken = default)
        {
            var files = await fileRepository.GetAllWithBanksAsync(cancellationToken);
            return files.Select(f => new FileMetadataDto
            {
                FileId = f.Id,
                FileName = f.FileName,
                PeriodStart = f.PeriodStart.ToString(DateTimeFormat),
                PeriodEnd = f.PeriodEnd.ToString(DateTimeFormat),
                UploadDate = f.UploadDate.ToString(DateTimeFormat),
                BankName = f.Bank.Name
            });
        }

        public async Task<FileInformationDto> GetFileInfoAsync(int fileId, CancellationToken cancellationToken = default)
        {
            var file = await fileRepository.FirstOrDefaultWithBankAsync(f => f.Id == fileId, cancellationToken)
                ?? throw new ItemNotFoundException("File");

            var records = await recordRepository.FindByWithClassAsync(r => r.File.Id == fileId, cancellationToken);
            var classTotals = await classTotalsRepository.FindByWithClassAsync(ct => ct.File.Id == fileId, cancellationToken); ;

            return new FileInformationDto()
            {
                File = MapFile(file),
                Records = MapRecords(records),
                ClassTotals = MapClassTotals(classTotals)
            };
        }
        private FileMetadataDto MapFile(FileMetadata file)
        {
            return new FileMetadataDto
            {
                FileId = file.Id,
                FileName = file.FileName,
                Currency = file.Currency,
                BankName = file.Bank.Name,
                PeriodStart = file.PeriodStart.ToString(DateTimeFormat),
                PeriodEnd = file.PeriodEnd.ToString(DateTimeFormat),
                ReportTitle = file.ReportTitle,
                UploadDate = file.UploadDate.ToString(DateTimeFormat)
            };
        }

        private IEnumerable<RecordDto> MapRecords(IEnumerable<Record> records)
        {
            return records.Select(r => new RecordDto
            {
                ClassId = r.Class.Id,
                ClassName = r.Class.Name,
                ActiveOpeningBalance = r.ActiveOpeningBalance,
                PasiveOpeningBalance = r.PasiveOpeningBalance,
                DebitTurnover = r.DebitTurnover,
                CreditTurnover = r.CreditTurnover,
                ActiveOutgoingBalance = r.ActiveOutgoingBalance,
                PassiveOutgoingBalance = r.PassiveOutgoingBalance,
                BankAccount = r.BankAccount
            });
        }

        private IEnumerable<ClassTotalsDto> MapClassTotals(IEnumerable<ClassTotals> classTotals)
        {
            return classTotals.Select(ct => new ClassTotalsDto
            {
                IsSubclass = ct.IsSubclass,
                ClassId = ct.ClassId,
                ClassName = ct.Class.Name,
                ActiveOpeningBalance = ct.ActiveOpeningBalance,
                PasiveOpeningBalance = ct.PasiveOpeningBalance,
                DebitTurnover = ct.DebitTurnover,
                CreditTurnover = ct.CreditTurnover,
                ActiveOutgoingBalance = ct.ActiveOutgoingBalance,
                PassiveOutgoingBalance = ct.PassiveOutgoingBalance,
                Subclass = ct.Subclass
            });
        }
    }
}
