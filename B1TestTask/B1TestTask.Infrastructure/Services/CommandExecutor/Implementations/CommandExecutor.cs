using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace B1TestTask.Infrastructure.Services.CommandExecutor.Implementations
{
    internal class CommandExecutor : ICommandExecutor
    {
        private readonly string connectionString;
        private readonly ILogger logger;

        public CommandExecutor(string connectionString, ILogger logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }

        public async Task ExecuteCommand(string command)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommand = new SqlCommand(command, connection))
                {
                    try
                    {
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        logger.LogError($"Error executing command: {ex.Message}");
                        logger.LogError($"Command: {command.Trim()}");
                    }
                }
            }
        }
    }
}
