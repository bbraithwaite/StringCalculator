using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
    public interface IDelimiterParser
    {
        /// <summary>
        /// Takes the string sequence then extracts and appends any custom delimiters to the array of valid delimiters 
        /// </summary>
        /// <param name="stringNumbers">The numbers string</param>
        /// <remarks>
        /// possible formats are:
        /// //;\n1;2 
        /// //[***]\n1***2***3
        /// //[*][%]\n1*2%3
        /// </remarks>
        IEnumerable<string> Parse(string stringNumbers);
    }
}
