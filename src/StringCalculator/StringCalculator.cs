using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator : IStringCalculator
    {
        private const int MAX_VALID_NUMBER = 1000;

        private static readonly List<String> _delimiters = new List<String>() { ",", "\n" };

        private readonly IConsole _console;

        private readonly IDelimiterParser _delimiterParser;

        public StringCalculator()
        {
            this._console = new ConsoleWrapper();
            this._delimiterParser = new SubStringDelimiterParser();
        }

        public StringCalculator(IConsole console, IDelimiterParser delimiterParser)
        {
            this._console = console;
            this._delimiterParser = delimiterParser;
        }

        public int Add(string stringNumbers)
        {
            if (string.IsNullOrEmpty(stringNumbers)) return 0;

            _delimiters.AddRange(_delimiterParser.Parse(stringNumbers));

            IEnumerable<int> parsedNumbers = ParseStringNumbers(RemoveCustomDelimiterPrefix(stringNumbers));
            CheckForNegatives(parsedNumbers);

            int sum = parsedNumbers.Where(n => n > 0 && n < MAX_VALID_NUMBER).Sum();

            WriteToConsole(sum);

            return sum;
        }

        private static void CheckForNegatives(IEnumerable<int> parsedNumbers)
        {
            IEnumerable<int> negativeNumbers = parsedNumbers.Where(n => n < 0).Select(n => n);

            if (negativeNumbers.Any())
            {
                throw new Exception(GetExceptionMessage(negativeNumbers));
            }
        }

        private void WriteToConsole(int sum)
        {
            if (_console != null)
            {
                _console.WriteLine(sum.ToString());
            }
        }

        private static string RemoveCustomDelimiterPrefix(string numbers)
        {
            if (numbers.StartsWith("//"))
            {
                string[] sections = numbers.Split('\n');
                if (sections.Length > 1)
                {
                    return sections[1];
                }
            }
            return numbers;
        }

        private static IEnumerable<int> ParseStringNumbers(string numbers)
        {
            List<int> validIntegers = new List<int>();
            string[] numberList = numbers.Split(_delimiters.ToArray(), StringSplitOptions.None);

            for (int i = 0; i < numberList.Length; i++)
            {
                int value = 0;
                if (int.TryParse(numberList[i], out value)) validIntegers.Add(value);
            }

            return validIntegers;
        }

        private static string GetExceptionMessage(IEnumerable<int> negativeNumbers)
        {
            return string.Format("negatives not allowed {0}", string.Join(",", negativeNumbers));
        }
    }
}