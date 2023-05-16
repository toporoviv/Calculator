using Calculator.MVVM.Exceptions;
using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
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
                            if (GetOperationPriority(value) <=
                                GetOperationPriority(operationsStack.Peek())) expression += operationsStack.Pop() + " ";
                        }

                        operationsStack.Push(value);
                    }
                }
            }

            while (operationsStack.Count != 0) expression += operationsStack.Pop() + " ";

            return expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid()
        {
            return _expressionValidator.IsValidExpression(_expression);
        }

        private int GetOperationPriority(string operation)
        {
            if (EngineeringCalculator.Operations.ContainsKey(operation)) return EngineeringCalculator.Operations[operation];
            else throw new OperationNotExistException($"операции {operation} не существует");
        }
    }
}
