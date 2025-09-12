using B1TestTask.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Infrastructure.Services
{
    internal class CommandExecutor : ICommandExecutor
    {
        private readonly string[] commandDividers = ["GO", "go", "Go", "gO"];
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

                // Разделяем скрипт на отдельные команды по "GO"
                string[] commands = command.Split(commandDividers,
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (string commandText in commands)
                {
                    if (!string.IsNullOrWhiteSpace(commandText))
                    {
                        using (var sqlCommand = new SqlCommand(commandText, connection))
                        {
                            try
                            {
                                await sqlCommand.ExecuteNonQueryAsync();
                            }
                            catch (SqlException ex)
                            {
                                logger.LogError($"Error executing command: {ex.Message}");
                                logger.LogError($"Command: {commandText.Trim()}");
                            }
                        }
                    }                  
                }
            }
        }
    }
}
