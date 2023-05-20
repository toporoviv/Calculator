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

        public virtual double GetAnswer(IExpression notation, string expression)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (notation is null) throw new ArgumentNullException();

            var newExpression = notation.GetExpression(expression);

            int index = 0;
            while (index < newExpression.Count && newExpression.Count != 1)
            {
                if (IsOperator(newExpression[index]))
                {
                    var arg1 = double.Parse(newExpression[index - 2]);
                    var arg2 = double.Parse(newExpression[index - 1]);
                    var result = _binaryOperations[newExpression[index]](arg1, arg2);

                    newExpression.RemoveAt(index - 2);
                    newExpression.RemoveAt(index - 2);
                    newExpression[index - 2] = result.ToString();
                    index = -1;
                }

                index++;
            }

            return double.Parse(newExpression[0]);
        }

        public virtual bool IsOperator(string operation)
        {
            return Operations.ContainsKey(operation);
        }
    }
}
