namespace B1TestTask.Infrastructure.Services.FileImporter
{
    public interface IFileImporter
    {
        /// <summary>
        /// Импортирует файл в бд
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="tableName">Название таблицы, в которую производится импорт</param>
        /// <returns></returns>
        public Task ImportFileAsync(string filePath, string tableName);
    }
}
