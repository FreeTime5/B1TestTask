namespace B1TestTask.Services.Services.TaskService
{
    public interface ITaskService
    {
        /// <summary>
        /// Производи импорт файлов
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns></returns>
        public Task ImportFiles(string filePath);

        /// <summary>
        /// Объединяет файлы в один
        /// </summary>
        /// <param name="subString">Подстрок, которая будет искаться для игнорирования строк</param>
        /// <returns></returns>
        public Task<int> UnionFiles(string subString = "");

        /// <summary>
        /// Выполняет подсчет суммы целых чисел и медианы дробных чисел через хранимаю процедуру
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string, object>> GetSumAndMedian();

        /// <summary>
        /// Получает пути всех файлов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllFiles();

        /// <summary>
        /// Создает файл
        /// </summary>
        /// <returns></returns>
        public Task CreateFile();
    }
}
