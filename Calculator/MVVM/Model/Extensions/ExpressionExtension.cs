using Calculator.MVVM.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Extensions
{
    public static class ExpressionExtension
    {
        public static string NormalizedExpression(this string expression)
        {
            var currentExpression = expression.Replace(" ", string.Empty);
            if (currentExpression != string.Empty)
            {
                currentExpression = currentExpression.Replace("--", "+");
                currentExpression = Regex.Replace(currentExpression, RegexHelper.PatternForReplace, match =>
                {
                    var str = match.Value.Split('-');
                    if (str[0] == ")") return $"{str[0]}+(0-{str[1]})";

                    return $"{str[0]}(0-{str[1]})";
                });
                if (currentExpression.StartsWith("-")) currentExpression = $"0{currentExpression}";
                else currentExpression = $"0+{currentExpression}";
            }

            return currentExpression;
        }

        public static string EnsureIsNotNullAndNotEmpty(this string expression)
        {
            if (string.IsNullOrEmpty(expression)) throw new ArgumentNullException();

            return expression;
        }

        public static string EnsureScopesArePlacedCorrectly(this string expression)
        {
            int leftScopeCount = 0, rightScopeCount = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(') leftScopeCount++;
                else if (expression[i] == ')') rightScopeCount++;

                if (rightScopeCount > leftScopeCount) throw new BadScopeException("Некорректное расставлены скобки");
            }

            if (leftScopeCount != rightScopeCount) throw new BadScopeException("Количество открывающихся и закрывающихся скобок различно");

            return expression;
        }

        public static string EnsureNumberOfOperandsAndOperationsIsCorrect(this string expression)
        {
            int digitCount = 0, operationCount = 0;

            var tempString = Regex.Replace(expression, RegexHelper.Pattern, match =>
            {
                if (match.Value.Any(x => char.IsDigit(x))) digitCount++;
                else operationCount++;
                return string.Empty;
            });

            int totalScopeCount = expression.Count(x => x == '(') + expression.Count(x => x == ')');

            if (operationCount - totalScopeCount != digitCount - 1) throw new NumberOfOperandsAndOperationsException("Некорректное кол-во операндов и операций");

            if (tempString.Length > 0) throw new Exception();

            return expression;
        }
    }
}
