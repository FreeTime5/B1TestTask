namespace B1TestTask2.Services.Services.FileLoader
{
    public interface IFileLoader
    {
        public Task LoadFileAsync(string filename, Stream readStream, CancellationToken cancellationToken = default);
    }
}
