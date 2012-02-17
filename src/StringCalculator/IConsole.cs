using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
    public interface IConsole
    {
        void WriteLine(string sum);
        string ReadLine();
    }
}
