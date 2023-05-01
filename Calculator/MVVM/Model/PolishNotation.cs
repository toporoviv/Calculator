using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class PolishNotation : IExpressionBuilder
    {
        private readonly string _expression;

        public PolishNotation(string expression)
        {
            _expression = expression.Replace(" ", string.Empty);
            if (_expression != string.Empty && !_expression.StartsWith("0+")) _expression = $"0+{_expression}";
        }

        public List<string> GetExpression()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Dictionary<string, int> operations)
        {
            if (_expression == null) return false;
            if (_expression == string.Empty) return true;

            int leftScopeCount = 0, rightScopeCount = 0;

            for (int i = 0; i < _expression.Length; i++)
            {
                if (_expression[i] == '(') leftScopeCount++;
                else if (_expression[i] == ')') rightScopeCount++;

                if (rightScopeCount > leftScopeCount) return false;
            }

            if (leftScopeCount != rightScopeCount) return false;

            int digitCount = 0, operationCount = 0;

            var tempString = Regex.Replace(_expression, @"(\d+\.\d+)|(\d+)|([\+\-\*/\(\)])", match =>
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
