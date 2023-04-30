using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class PolishNotation : IExpressionBuilder
    {
        private readonly string _expression;

        public PolishNotation(string expression)
        {
            _expression = expression;
        }

        public List<string> GetExpression()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Dictionary<string, int> operations)
        {
            throw new NotImplementedException();
        }
    }
}
