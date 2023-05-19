using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Abstract
{
    public abstract class BaseExpression : IExpression
    {
        protected readonly string _expression;
        protected readonly IExpressionValidator _expressionValidator;

        public BaseExpression(IExpressionValidator expressionValidator, string expression)
        {
            _expressionValidator = expressionValidator;
            _expression = _expressionValidator.GetNormalizedExpression(expression);
        }

        public abstract List<string> GetExpression();

        public virtual bool IsValid() => _expressionValidator.IsValidExpression(_expression);
    }
}
