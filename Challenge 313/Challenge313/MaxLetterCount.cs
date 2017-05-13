using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge313
{
    public class MaxLetterCount
    {
        public char Letter;
        public int mostOccurances { get; set; }
        public string wordMostOccurances { get; set; }
        public int totalOccurances { get; set; }
        public MaxLetterCount next { get; set; }

        public MaxLetterCount(char letter)
        {
            Letter = letter;
            mostOccurances = 0;
        }
    }
}
