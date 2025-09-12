using B1TestTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Infrastructure.Services
{
    internal class ConsoleImportFileProgressReporter : IImportFileProgressReporter
    {
        public void ReportComplete(int totalRecords, TimeSpan elapsedTime)
        {
            Console.WriteLine($"Импорт завершен!");
            Console.WriteLine($"Всего записей: {totalRecords}");
            Console.WriteLine($"Затраченное время: {elapsedTime.TotalSeconds:F2} секунд");
        }

        public void ReportError(string errorMessage, int lineNumber)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ОШИБКА: {errorMessage}");
            if (lineNumber > 0)
            {
                Console.WriteLine($"Строка: {lineNumber}");
            }
            Console.ResetColor();
        }

        public void ReportProgress(int current, int total, string status)
        {
            double precentage = total > 0 ? (double)current / total * 100 : 0;

            Console.SetCursorPosition(0, Console.CursorTop);

            Console.Write($"Прогресс: {current}/{total}, осталось - {total - current} ({precentage:F1}%) - {status}");
        }
    }
}
