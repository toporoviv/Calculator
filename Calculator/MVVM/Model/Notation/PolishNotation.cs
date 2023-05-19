using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator.MVVM.Model.Notation
{
    public class PolishNotation : BaseExpression
    {
        public PolishNotation(IExpressionValidator expressionValidator, string expression) : base(expressionValidator, expression)
        {
        }

        public override List<string> GetExpression()
        {
            if (!IsValid()) throw new FormatException();

            string expression = string.Empty;

            var matches = Regex.Matches(_expression, RegexHelper.Pattern);

            var operationsStack = new Stack<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                string value = matches[i].Value;

                // Если это число
                if (value.Any(x => char.IsDigit(x))) expression += value + " ";
                else // оператор
                {
                    if (value == "(") operationsStack.Push(value);
                    else if (value == ")")
                    {
                        var stackElement = operationsStack.Pop();

                        while (stackElement != "(")
                        {
                            expression += stackElement + " ";
                            stackElement = operationsStack.Pop();
                        }
                    }
                    else
                    {
                        if (operationsStack.Count > 0)
                        {
                            if (Model.Helper.OperationHelper.GetOperationPriority(value) <=
                                Model.Helper.OperationHelper.GetOperationPriority(operationsStack.Peek())) expression += operationsStack.Pop() + " ";
                        }

                        operationsStack.Push(value);
                    }
                }
            }

            while (operationsStack.Count != 0) expression += operationsStack.Pop() + " ";

            return expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
