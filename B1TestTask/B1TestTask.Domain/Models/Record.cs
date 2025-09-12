using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace B1TestTask.Domain.Models
{
    public class Record
    {
        public DateTime Date { get; set; }
        public string LatinChars { get; set; }
        public string RussianChars { get; set; }
        public int IntNumber { get; set; }
        public double DoubleNumber { get; set; }
    }
}
