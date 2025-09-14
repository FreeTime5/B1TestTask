using B1TestTask2.Domain.Models;
using B1TestTask2.Services.Models;
using B1TestTask2.Services.Services.ExcelParser;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;

namespace B1TestTask2.Infrastructure.Services
{
    internal class ExcelParser : IExcelParser
    {
        public ParsingResult ParseExecelFile(string fileName, Stream readStream)
        {
            IWorkbook workbook;
            try
            {
                workbook = fileName.EndsWith(".xlsx")
                    ? new XSSFWorkbook(readStream, true)
                    : new HSSFWorkbook(readStream, true);
            }
            finally
            {
                if (readStream != null)
                {
                    readStream.Dispose();
                }
            }

            var sheet = workbook.GetSheetAt(0);

            var file = CreateFileMetadata(sheet, fileName);

            var records = new List<Record>();
            var classTotals = new List<ClassTotals>();

            for (var rowIndex = 8; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null)
                {
                    continue;
                }

                var firstCellValue = GetCellValue(row.GetCell(0));

                if (firstCellValue == null)
                {
                    continue;
                }

                if (firstCellValue.StartsWith("КЛАСС"))
                {
                    var totals = ParseClass(row, firstCellValue, file);
                    classTotals.Add(totals);
                    continue;
                }

                if (firstCellValue.StartsWith("ПО КЛАССУ"))
                {
                    UpdateClassTotal(row, classTotals.Last(ct => !ct.IsSubclass));
                    continue;
                }

                if (IsSubClassTotals(firstCellValue))
                {
                    var totals = ParseSubClass(row, classTotals.Last(ct => !ct.IsSubclass), file);
                    classTotals.Add(totals);
                    continue;
                }

                if (IsAccountCode(firstCellValue))
                {
                    var record = ParseAccountRecord(row, classTotals.Last(ct => !ct.IsSubclass).Class, file);
                    records.Add(record);
                }
            }

            return new ParsingResult()
            {
                ClassTotals = classTotals,
                File = file,
                Records = records
            };
        }

        private FileMetadata CreateFileMetadata(ISheet sheet, string fileName)
        {
            var bankName = GetCellValue(sheet.GetRow(0).GetCell(0));
            var bank = new Bank()
            {
                Name = bankName
            };

            var reportTitel = GetCellValue(sheet.GetRow(1).GetCell(0));
            var currency = GetCellValue(sheet.GetRow(5).GetCell(6));

            var file = new FileMetadata()
            {
                FileName = fileName,
                Bank = bank,
                ReportTitle = reportTitel,
                Currency = currency
            };

            ParsePeriod(sheet.GetRow(2).GetCell(0), file);
            return file;
        }

        private void ParsePeriod(ICell cell, FileMetadata file)
        {
            var parts = GetCellValue(cell).Split(' ');
            var dates = parts.Where(p => DateTime.TryParseExact(
                p,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
                .Select(d => DateTime.ParseExact(d, "dd.MM.yyyy", CultureInfo.InvariantCulture));

            if (dates.Count() == 2)
            {
                file.PeriodStart = dates.ElementAt(0);
                file.PeriodEnd = dates.ElementAt(1);
            }
        }

        private ClassTotals ParseClass(IRow row, string classText, FileMetadata file)
        {
            var parts = classText.Split(' ').Where(p => !string.IsNullOrEmpty(p));
            var id = parts.Count() > 1 ? parts.ElementAt(1) : "0";
            var name = string.Join(' ', parts);

            var operationsClass = new Class()
            {
                Id = id,
                Name = name
            };
            return new ClassTotals
            {
                Class = operationsClass,
                File = file
            };
        }

        private Record ParseAccountRecord(IRow row, Class recordClass, FileMetadata file)
        {
            return new Record
            {
                BankAccount = GetCellValue(row.GetCell(0)),
                ActiveOpeningBalance = GetNumericValue(row.GetCell(1)) ?? 0,
                PasiveOpeningBalance = GetNumericValue(row.GetCell(2)) ?? 0,
                DebitTurnover = GetNumericValue(row.GetCell(3)) ?? 0,
                CreditTurnover = GetNumericValue(row.GetCell(4)) ?? 0,
                ActiveOutgoingBalance = GetNumericValue(row.GetCell(5)) ?? 0,
                PassiveOutgoingBalance = GetNumericValue(row.GetCell(6)) ?? 0,
                Class = recordClass,
                File = file
            };
        }

        private ClassTotals ParseSubClass(IRow row, ClassTotals classTotals, FileMetadata file)
        {
            return new ClassTotals
            {
                Subclass = GetCellValue(row.GetCell(0)),
                IsSubclass = true,
                ActiveOpeningBalance = GetNumericValue(row.GetCell(1)) ?? 0,
                PasiveOpeningBalance = GetNumericValue(row.GetCell(2)) ?? 0,
                DebitTurnover = GetNumericValue(row.GetCell(3)) ?? 0,
                CreditTurnover = GetNumericValue(row.GetCell(4)) ?? 0,
                ActiveOutgoingBalance = GetNumericValue(row.GetCell(5)) ?? 0,
                PassiveOutgoingBalance = GetNumericValue(row.GetCell(6)) ?? 0,
                Class = classTotals.Class,
                File = file
            };
        }

        private void UpdateClassTotal(IRow row, ClassTotals classTotal)
        {
            classTotal.ActiveOpeningBalance = GetNumericValue(row.GetCell(1)) ?? 0;
            classTotal.PasiveOpeningBalance = GetNumericValue(row.GetCell(2)) ?? 0;
            classTotal.DebitTurnover = GetNumericValue(row.GetCell(3)) ?? 0;
            classTotal.CreditTurnover = GetNumericValue(row.GetCell(4)) ?? 0;
            classTotal.ActiveOutgoingBalance = GetNumericValue(row.GetCell(5)) ?? 0;
            classTotal.PasiveOpeningBalance = GetNumericValue(row.GetCell(6)) ?? 0;
        }

        private string GetCellValue(ICell cell)
        {
            if (cell == null) return string.Empty;

            return cell.CellType switch
            {
                CellType.String => cell.StringCellValue.Trim(),
                CellType.Numeric => cell.NumericCellValue.ToString(CultureInfo.InvariantCulture),
                CellType.Boolean => cell.BooleanCellValue.ToString(),
                CellType.Formula => GetFormulaValue(cell),
                _ => string.Empty
            };
        }

        private decimal? GetNumericValue(ICell cell)
        {
            if (cell == null) return null;

            if (cell.CellType == CellType.Numeric)
                return (decimal)cell.NumericCellValue;

            if (cell.CellType == CellType.String &&
                decimal.TryParse(cell.StringCellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                return result;

            return null;
        }

        private string GetFormulaValue(ICell cell)
        {
            try
            {
                return cell.StringCellValue;
            }
            catch
            {
                return cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
            }
        }

        private bool IsAccountCode(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().All(char.IsDigit);
        }

        private bool IsSubClassTotals(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().All(char.IsDigit) && value.Length == 2;
        }
    }
}
