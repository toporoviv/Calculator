using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class Calculator
    {
        public static readonly Dictionary<string, int> _operations = new Dictionary<string, int>()
        {
            { "(", 0 },
            { ")", 0 },
            { "*", 1 },
            { "/", 1 },
            { "+", 2 },
            { "-", 2 },
        };

        public double GetAnswer(string notation)
        {
            throw new NotImplementedException();
        }
    }
}
