using B1TestTask.Domain.Interfaces;
using B1TestTask.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;
using System.Globalization;
using System.Numerics;

namespace B1TestTask.Infrastructure.Services
{
    internal class FileImporter : IFileImporter
    {
        private readonly string connectionString;
        private readonly IImportFileProgressReporter progressReporter;

        public FileImporter(string connectionString, IImportFileProgressReporter progressReporter)
        {
            this.connectionString = connectionString;
            this.progressReporter = progressReporter;
        }

        public async Task ImportFileAsync(string filePath, string tableName)
        {
            var startTime = DateTime.Now;
            var totalLines = 0;
            var processedLines = 0;
            var currentLine = 0;

            try
            {
                totalLines = await CountLinesAsync(filePath);
                progressReporter.ReportProgress(0, totalLines, "Импорт начался:");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                            

                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.BatchSize = 1000;
                        bulkCopy.NotifyAfter = 100;
                        bulkCopy.ColumnMappings.Add("Date", "Date");
                        bulkCopy.ColumnMappings.Add("LatinChars", "LatinChars");
                        bulkCopy.ColumnMappings.Add("RussianChars", "RussianChars");
                        bulkCopy.ColumnMappings.Add("IntNumber", "IntNumber");
                        bulkCopy.ColumnMappings.Add("DoubleNumber", "DoubleNumber");

                        bulkCopy.SqlRowsCopied += (sender, e) =>
                        {
                            progressReporter.ReportProgress(currentLine, totalLines, $"Импортировано {currentLine} из {totalLines} записей");
                        };

                        using (var reader = new StreamReader(filePath))
                        {
                            var dataTable = CreateDataTable();

                            string? line;
                            while((line = await reader.ReadLineAsync()) != null)
                            {
                                currentLine++;
                                try
                                {
                                    var record = ParseLine(line);
                                    AddRecordToDataTable(dataTable, record);

                                    if (dataTable.Rows.Count >= bulkCopy.BatchSize)
                                    {
                                        await bulkCopy.WriteToServerAsync(dataTable);
                                        dataTable.Clear();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    progressReporter.ReportError($"Ошибка в строке {currentLine}: {ex.Message}", processedLines + 1);
                                }
                            }

                            if (dataTable.Rows.Count > 0)
                            {
                                await bulkCopy.WriteToServerAsync(dataTable);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add("Date", typeof(DateTime));
            dataTable.Columns.Add("LatinChars", typeof(string));
            dataTable.Columns.Add("RussianChars", typeof(string));
            dataTable.Columns.Add("IntNumber", typeof(int));
            dataTable.Columns.Add("DoubleNumber", typeof(double));

            return dataTable;
        }

        private Record ParseLine(string line)
        {
            var values = line.Split("||");
            var date = DateTime.ParseExact(values[0], "dd.MM.yyyy", CultureInfo.CurrentCulture);
            var latinChars = values[1];
            var russianChars = values[2];
            var intNumber = int.Parse(values[3]);
            var doubleNumber = double.Parse(values[4]);
            return new Record()
            {
                Date = date,
                LatinChars = latinChars,
                RussianChars = russianChars,
                IntNumber = intNumber,
                DoubleNumber = doubleNumber

            };
        }

        private void AddRecordToDataTable(DataTable dataTable, Record record)
        {
            var row = dataTable.NewRow();

            row["Date"] = record.Date;
            row["LatinChars"] = record.LatinChars;
            row["RussianChars"] = record.RussianChars;
            row["IntNumber"] = record.IntNumber;
            row["DoubleNumber"] = record.DoubleNumber;

            dataTable.Rows.Add(row);
        }

        private async Task<int> CountLinesAsync(string filePath)
        {
            var count = 0;

            using (var reader = new StreamReader(filePath))
            {
                while (await reader.ReadLineAsync() != null)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
