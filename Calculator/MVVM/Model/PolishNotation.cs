using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public List<string> GetExpression()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Dictionary<string, int> operations)
        {
            if (_expression == null) return false;
            if (_expression == string.Empty) return true;

            int leftCount = 0, rightCount = 0;

            for (int i = 0; i < _expression.Length; i++)
            {
                if (_expression[i] == '(') leftCount++;
                else if (_expression[i] == ')') rightCount++;

                if (rightCount > leftCount) return false;
            }

            if (leftCount != rightCount) return false;

            int digitCount = 0;

            var tempString = Regex.Replace(_expression, @"(-?\d+)", match =>
            {
                digitCount++;
                return string.Empty;    
            });

            int operationCount = 0;

            foreach (var i in operations)
            {
                tempString = tempString.Replace(i.Key, string.Empty);
                operationCount++;
            }

            if (operationCount != digitCount - 1) return false;

            if (tempString.Length > 0) return false;

            return true;
        }
    }
}
