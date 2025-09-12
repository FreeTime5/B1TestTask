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
        private const string LATINCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";//
        private const string RUSSIANCHARS = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const int MAXINT = 100000000;
        private const double MAXDOUBLE = 20;
        private const int LATINCHARSCOUNT = 10;
        private const int RUSSIANCHARSCOUNT = 10;
        private const string DIVIDER = "||";
        private Random random;
        private DateTime startDate;
        private DateTime endDate;

        public TextService()
        {
            random = new Random();
            var t = DateTime.Now;
            startDate = t.AddYears(-5);
            endDate = t;
        }

        public string GenerateString()
        {
            var randomDateTime = GenerateDayTime().ToString("dd.MM.yyyy");
            var randomLatinChars = GenerateLatinCharacters();
            var randomRussianChars = GenerateRussianCharacters();
            var randomIntNumber = random.Next(MAXINT);
            var randomDoubleNumber = (random.NextDouble()*MAXDOUBLE).ToString();

            var sb = new StringBuilder();
            sb.Append(randomDateTime);
            sb.Append(DIVIDER);
            sb.Append(randomLatinChars);
            sb.Append(DIVIDER);
            sb.Append(randomRussianChars);
            sb.Append(DIVIDER);
            sb.Append(randomIntNumber);
            sb.Append(DIVIDER);
            sb.Append(randomDoubleNumber);
            sb.Append(DIVIDER);

            return sb.ToString();
        }

        private DateTime GenerateDayTime()
        {
            var dateRange = endDate - startDate;
            var tatalDays = (int)dateRange.TotalDays;//
            int randomDays = random.Next(tatalDays);

            return startDate.AddDays(randomDays);
        }

        /// <summary>
        /// Генерация символов через цикл
        /// </summary>
        /// <returns></returns>
        private string GenerateLatinCharacters()
        {
            var chars = new char[LATINCHARSCOUNT];      
            for (var i = 0; i < LATINCHARSCOUNT; i++)
            {
                chars[i] = LATINCHARS[random.Next(LATINCHARS.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Генерация с помощью LINQ
        /// </summary>
        /// <returns></returns>
        private string GenerateRussianCharacters()
        {
            var chars = new char[RUSSIANCHARSCOUNT];    
            chars.Select(c => random.Next(RUSSIANCHARS.Length));

            return new string(chars);
        }
    }
}
