using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public abstract class BaseCalculator : ICalculator
    {
        protected static readonly double _eps = 0.0001;

        protected static readonly Dictionary<string, Func<double, double, double>> _binaryOperations =
            new Dictionary<string, Func<double, double, double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => Math.Abs(y) < _eps ? throw new DivideByZeroException() : x / y },
                { "^", (x, y) => Math.Pow(x, y) }
            };

        public static Dictionary<string, int> Operations { get; } = new Dictionary<string, int>()
        {
            { "(", 0 },
            { ")", 1 },
            { "+", 2 },
            { "-", 2 },
            { "*", 3 },
            { "/", 3 },
            { "^", 3 }
        };

        public abstract double GetAnswer(IExpression notation);
    }
}
