using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class Calculator
    {
        private static readonly double _eps = 0.0001;

        private static readonly Dictionary<string, Func<double, double, double>> _binaryOperations =
            new Dictionary<string, Func<double, double, double>>
            {
                { "+", (x, y) => x + y },
                { "-", (x, y) => x - y },
                { "*", (x, y) => x * y },
                { "/", (x, y) => Math.Abs(y) < _eps ? throw new DivideByZeroException() : x / y }
            };

        public static Dictionary<string, int> Operations { get; } = new Dictionary<string, int>()
        {
            { "(", 0 },
            { ")", 1 },
            { "+", 2 },
            { "-", 2 },
            { "*", 3 },
            { "/", 3 }
        };

        public double GetAnswer(IExpressionBuilder notation)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (notation is null) throw new ArgumentNullException();

            var expression = notation.GetExpression();

            int index = 0;
            while (index < expression.Count && expression.Count != 1)
            {
                if (IsOperator(expression[index]))
                {
                    var arg1 = double.Parse(expression[index - 2]);
                    var arg2 = double.Parse(expression[index - 1]);
                    var result = _binaryOperations[expression[index]](arg1, arg2);

                    expression.RemoveAt(index - 2);
                    expression.RemoveAt(index - 2);
                    expression[index - 2] = result.ToString();
                    index = -1;
                }

                index++;
            }

            return double.Parse(expression[0]);
        }

        private bool IsOperator(string expression)
        {
            return Operations.ContainsKey(expression);
        }
    }
}
