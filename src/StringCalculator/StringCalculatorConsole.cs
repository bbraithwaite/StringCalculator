using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculatorConsole
    {
        private readonly IStringCalculator _calculator;

        private readonly IConsole _console;

        private readonly Regex CalculatorInput = new Regex("^scalc '(.*)'$");

        public StringCalculatorConsole(IStringCalculator calculator, IConsole console)
        {
            _calculator = calculator;
            _console = console;
        }

        public void Main(string[] args)
        {
            string input = args[0];

            Match match = CalculatorInput.Match(input);

            if (match.Success)
            {
                DisplaySum(match.Groups[1].Value);
                ReadNextInput();
            }
        }

        private void DisplaySum(string numbers)
        {
            _console.WriteLine(string.Format("The result is {0}", _calculator.Add(numbers)));
        }

        private void ReadNextInput()
        {
            _console.WriteLine("another input please");
            string readLineInput = _console.ReadLine();

            if (!string.IsNullOrEmpty(readLineInput))
            {
                DisplaySum(readLineInput);
                ReadNextInput();
            }
        }
    }
}
