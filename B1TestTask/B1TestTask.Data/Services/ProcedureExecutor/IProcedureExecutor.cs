namespace B1TestTask.Infrastructure.Services.ProcedureExecutor
{
    public interface IProcedureExecutor
    {
        /// <summary>
        /// Выполняет хранимую процедуру
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string, object>> ExecuteProcedure();

        /// <summary>
        /// Создает хранимую процедуру
        /// </summary>
        /// <returns></returns>
        public Task CreateProcedure();

        /// <summary>
        /// Проверяет наличие хранимой процедуры
        /// </summary>
        /// <returns></returns>
        public Task<bool> ProcedureExists();
    }
}
