namespace B1TestTask.Data.Interfaces
{
    public interface IFileReader
    {
        /// <summary>
        /// Читает полностью файл и возвращает прочитанную строку
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns></returns>
        public Task<string?> ReadFile(string filePath);
    }
}
