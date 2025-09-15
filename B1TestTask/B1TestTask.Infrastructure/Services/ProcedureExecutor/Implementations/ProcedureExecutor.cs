using B1TestTask.Data.Interfaces;
using B1TestTask.Infrastructure.Services.CommandExecutor;
using Microsoft.Data.SqlClient;

namespace B1TestTask.Infrastructure.Services.ProcedureExecutor.Implementations
{
    internal class ProcedureExecutor : IProcedureExecutor
    {
        private const string PROCEDURENAME = "dbo.SumIntAndFindMedian";
        private readonly string connectionString;
        private readonly IFileReader fileReader;
        private readonly ICommandExecutor commandExecutor;

        public string ProcedureName
        {
            get
            {
                return PROCEDURENAME;
            }
        }

        public ProcedureExecutor(string connectionString,
            IFileReader fileReader,
            ICommandExecutor commandExecutor)
        {
            this.connectionString = connectionString;
            this.fileReader = fileReader;
            this.commandExecutor = commandExecutor;
        }

        public async Task<Dictionary<string, object>> ExecuteProcedure()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(PROCEDURENAME, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var totalSumParam = new SqlParameter("@TotalSum", System.Data.SqlDbType.BigInt)
                    {
                        Direction = System.Data.ParameterDirection.Output,

                    };
                    var medianParam = new SqlParameter("@Median", System.Data.SqlDbType.Decimal)
                    {
                        Direction = System.Data.ParameterDirection.Output,
                        Precision = 10,
                        Scale = 8
                    };

                    command.Parameters.AddRange([totalSumParam, medianParam]);

                    await command.ExecuteNonQueryAsync();

                    var result = new Dictionary<string, object>{
                        { "@TotalSum", command.Parameters["@TotalSum"].Value },
                        { "@Median", command.Parameters["@Median"].Value }
                    };

                    return result;
                }
            }
        }

        public async Task CreateProcedure()
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            var fileFolder = Path.Combine(projectDirectory, "B1TestTask.Infrastructure", "SqlScripts");
            var filePath = Directory.GetFiles(fileFolder)[0];

            var script = await fileReader.ReadFile(filePath);

            if (string.IsNullOrEmpty(script))
            {
                throw new Exception();
            }

            await commandExecutor.ExecuteCommand(script);
        }

        public async Task<bool> ProcedureExists()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                SELECT CASE 
                    WHEN OBJECT_ID(@procedureName, 'P') IS NOT NULL THEN 1 
                    ELSE 0 
                END";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@procedureName", PROCEDURENAME);

                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result) == 1;
                }
            }
        }
    }
}
