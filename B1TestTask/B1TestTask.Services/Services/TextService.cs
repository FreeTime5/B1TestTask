using B1TestTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Services.Services
{
    internal class TextService : ITextService
    {
        private const string LATINCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string RUSSIANCHARS = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private Random random;
        private DateTime startDate;
        private DateTime endDate;

        public TextService()
        {
            random = new Random();
            startDate = DateTime.Now.AddYears(-5);
            endDate = DateTime.Now;
        }

        public string GenerateString()
        {
            var builder = new StringBuilder();
            builder.Append(GenerateDayTime().ToString("dd.MM.yyyy"));
            builder.Append("||");
            builder.Append(GenerateLatinCharacters());
            builder.Append("||");
            builder.Append(GenerateRussianCharacters());
            builder.Append("||");
            builder.Append(random.Next(100000000));
            builder.Append("||");
            builder.Append((double)random.Next(2000000000) / 100000000);
            builder.Append("||");

            return builder.ToString();
        }

        private DateTime GenerateDayTime()
        {
            var dateRange = endDate - startDate;
            var tatalDays = (int)dateRange.TotalDays;
            int randomDays = random.Next(tatalDays + 1);

            return startDate.AddDays(randomDays);
        }

        private string GenerateLatinCharacters()
        {
            var chars = new char[10];
            for (var i = 0; i < 10; i++)
            {
                chars[i] = LATINCHARS[random.Next(LATINCHARS.Length)];
            }

            return new string(chars);
        }

        private string GenerateRussianCharacters()
        {
            var chars = new char[10];
            for (var i = 0; i < 10; i++)
            {
                chars[i] = RUSSIANCHARS[random.Next(RUSSIANCHARS.Length)];
            }

            return new string(chars);
        }
    }
}
