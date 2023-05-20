using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator.MVVM.Model.Notation
{
    public class PolishNotation : IExpression
    {
        public List<string> GetExpression(string expression)
        {
            expression = expression
                .NormalizedExpression()
                .EnsureIsNotNullAndNotEmpty()
                .EnsureScopesArePlacedCorrectly()
                .EnsureNumberOfOperandsAndOperationsIsCorrect();

            string resultExpression = string.Empty;

            var matches = Regex.Matches(expression, RegexHelper.Pattern);

            var operationsStack = new Stack<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                string value = matches[i].Value;

                // Если это число
                if (value.Any(x => char.IsDigit(x))) resultExpression += value + " ";
                else // оператор
                {
                    if (value == "(") operationsStack.Push(value);
                    else if (value == ")")
                    {
                        var stackElement = operationsStack.Pop();

                        while (stackElement != "(")
                        {
                            resultExpression += stackElement + " ";
                            stackElement = operationsStack.Pop();
                        }
                    }
                    else
                    {
                        if (operationsStack.Count > 0)
                        {
                            if (Model.Helper.OperationHelper.GetOperationPriority(value) <=
                                Model.Helper.OperationHelper.GetOperationPriority(operationsStack.Peek())) resultExpression += operationsStack.Pop() + " ";
                        }

                        operationsStack.Push(value);
                    }
                }
            }

            while (operationsStack.Count != 0) resultExpression += operationsStack.Pop() + " ";

            return resultExpression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
