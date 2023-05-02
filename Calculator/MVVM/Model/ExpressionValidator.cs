using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class ExpressionValidator : IExpressionValidator
    {
        public string GetNormalizedExpression(string expression)
        {
            var currentExpression = expression.Replace(" ", string.Empty);
            if (currentExpression != string.Empty)
            {
                if (currentExpression.StartsWith("-")) currentExpression = $"0{currentExpression}";
                else currentExpression = $"0+{currentExpression}";
            }

            return currentExpression;
        }

        public bool IsValidExpression(string expression)
        {
            if (expression == null) return false;
            if (expression == string.Empty) return true;

            int leftScopeCount = 0, rightScopeCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(') leftScopeCount++;
                else if (expression[i] == ')') rightScopeCount++;

                if (rightScopeCount > leftScopeCount) return false;
            }

            if (leftScopeCount != rightScopeCount) return false;

            int digitCount = 0, operationCount = 0;

            var tempString = Regex.Replace(expression, RegexHelper.Pattern, match =>
            {
                if (match.Value.Any(x => char.IsDigit(x))) digitCount++;
                else operationCount++;
                return string.Empty;
            });

            int totalScopeCount = leftScopeCount + rightScopeCount;

            if (operationCount - totalScopeCount != digitCount - 1) return false;

            if (tempString.Length > 0) return false;

            return true;
        }
    }
}
