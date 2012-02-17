using System;

namespace StringCalculator
{
    public class ConsoleWrapper : IConsole
    {
        public void WriteLine(string sum)
        {
            Console.WriteLine(sum);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
