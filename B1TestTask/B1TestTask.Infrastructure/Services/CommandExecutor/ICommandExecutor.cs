namespace B1TestTask.Infrastructure.Services.CommandExecutor
{
    public interface ICommandExecutor
    {
        /// <summary>
        /// Выполняет sql script
        /// </summary>
        /// <param name="command">Sql script</param>
        /// <returns></returns>
        public Task ExecuteCommand(string command);
    }
}
