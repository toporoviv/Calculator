using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class GeneralCalculator : BaseCalculator
    {
        public override double GetAnswer(IExpression notation)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var result = 0.0;

            var expression = notation.GetExpression();

            while (expression.Count != 1)
            {
                var fisrtArg = double.Parse(expression[0]);
                var secondArg = double.Parse(expression[2]);
                result = _binaryOperations[expression[1]](fisrtArg, secondArg);

                expression.RemoveAt(0);
                expression.RemoveAt(0);

                expression[0] = result.ToString();
            }

            return result;
        }
    }
}
