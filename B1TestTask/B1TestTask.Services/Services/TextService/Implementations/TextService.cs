using System.Text;

namespace B1TestTask.Services.Services.TextService.Implementations
{
    internal class TextService : ITextService
    {
        private const string LatinChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";//
        private const string RussianChars = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const int MaxInt = 100000000;
        private const double MaxDouble = 20;
        private const int LatinCharsCount = 10;
        private const int RussianCharsCount = 10;
        private const string Divider = "||";
        private Random random;
        private readonly DateTime startDate;
        private readonly DateTime endDate;

        public TextService()
        {
            random = new Random();
            endDate = DateTime.Now;
            startDate = endDate.AddYears(-5);
        }

        public string GenerateString()
        {
            var randomDateTime = GenerateDayTime().ToString("dd.MM.yyyy");
            var randomLatinChars = GenerateLatinCharacters();
            var randomRussianChars = GenerateRussianCharacters();
            var randomIntNumber = random.Next(MaxInt);
            var randomDoubleNumber = (random.NextDouble() * MaxDouble).ToString();

            var sb = new StringBuilder();
            sb.Append(randomDateTime);
            sb.Append(Divider);
            sb.Append(randomLatinChars);
            sb.Append(Divider);
            sb.Append(randomRussianChars);
            sb.Append(Divider);
            sb.Append(randomIntNumber);
            sb.Append(Divider);
            sb.Append(randomDoubleNumber);
            sb.Append(Divider);

            return sb.ToString();
        }

        private DateTime GenerateDayTime()
        {
            var dateRange = endDate - startDate;
            var tatalDays = (int)dateRange.TotalDays;
            int randomDays = random.Next(tatalDays);

            return startDate.AddDays(randomDays);
        }

        /// <summary>
        /// Генерация символов через цикл
        /// </summary>
        /// <returns></returns>
        private string GenerateLatinCharacters()
        {
            var chars = new char[LatinCharsCount];
            for (var i = 0; i < LatinCharsCount; i++)
            {
                chars[i] = LatinChars[random.Next(LatinChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Генерация с помощью LINQ
        /// </summary>
        /// <returns></returns>
        private string GenerateRussianCharacters()
        {
            var chars = new char[RussianCharsCount];
            chars.Select(c => RussianChars[random.Next(RussianChars.Length)]);

            return new string(chars);
        }
    }
}
