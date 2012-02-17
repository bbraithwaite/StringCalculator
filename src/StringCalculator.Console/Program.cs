namespace StringCalculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            StringCalculatorConsole sc = new StringCalculatorConsole(new StringCalculator(), new ConsoleWrapper());
            sc.Main(new string[] { "scalc '1'" });
        }
    }
}