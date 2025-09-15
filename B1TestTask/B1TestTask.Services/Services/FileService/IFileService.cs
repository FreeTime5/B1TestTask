namespace B1TestTask.Services.Services.FileService
{
    internal interface IFileService
    {
        /// <summary>
        /// Создает файл и возвращает stream для записи в него
        /// </summary>
        /// <returns>Sream для записи данных в файл</returns>
        public StreamWriter CreateFile();

        /// <summary>
        /// Объединяет файлы в один файл
        /// </summary>
        /// <returns></returns>
        public Task UnionFiles();

        /// <summary>
        /// Объединяет файлы и удаляет строки с подстрокой
        /// </summary>
        /// <param name="subString">Подстрока, которая будет искаться для удаления строки</param>
        /// <returns>Количество удаленных строк</returns>
        public Task<int> UnionFiles(string subString);

        /// <summary>
        /// Получает все пути к файлам в рабочей папке
        /// </summary>
        /// <returns>Пути к файлам</returns>
        public IEnumerable<string> GetAllFiles();
    }
}
