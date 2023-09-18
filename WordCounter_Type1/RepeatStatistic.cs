using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WordCounterLib
{
    public class RepeatStatistic : IComparable
    {
        public string Word { get; }
        public int Number { get; }

        public RepeatStatistic(string word, int number)
        {
            Word = word;
            Number = number;
        }
        public int CompareTo(object? o)
        {
            if (o is RepeatStatistic stat) 
                return Number.CompareTo(stat.Number);
                throw new ArgumentException("Incorrect parameter value");
        }
    }
}
