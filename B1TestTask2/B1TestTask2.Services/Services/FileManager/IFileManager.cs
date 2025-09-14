using B1TestTask2.Services.Dtos.Response;

namespace B1TestTask2.Services.Services.FileManager
{
    public interface IFileManager
    {
        public Task<IEnumerable<FileMetadataDto>> GetAllFilesAsync(CancellationToken cancellationToken = default);

        public Task<FileInformationDto> GetFileInfoAsync(int fileId, CancellationToken cancellationToken = default);
    }
}
