using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class EngineeringCalculator : BaseCalculator
    {
        public override double GetAnswer(IExpression notation)
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
