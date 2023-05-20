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
        public override double GetAnswer(IExpression notation, string expression)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            var result = 0.0;

            var currentExpression = notation.GetExpression(expression);

            while (currentExpression.Count != 1)
            {
                var fisrtArg = double.Parse(currentExpression[0]);
                var secondArg = double.Parse(currentExpression[2]);
                result = _binaryOperations[currentExpression[1]](fisrtArg, secondArg);

                currentExpression.RemoveAt(0);
                currentExpression.RemoveAt(0);

                currentExpression[0] = result.ToString();
            }

            return result;
        }
    }
}
