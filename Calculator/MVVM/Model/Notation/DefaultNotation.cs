using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.MVVM.Model.Notation
{
    public class DefaultNotation : BaseExpression
    {
        public DefaultNotation(IExpressionValidator expressionValidator, string expression) : base(expressionValidator, expression)
        {
        }

        public override List<string> GetExpression()
        {
            if (!IsValid()) throw new FormatException();

            var matches = Regex.Matches(_expression, RegexHelper.Pattern);

            var expression = new List<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                expression.Add(matches[i].Value);
            }

            return expression;
        }
    }
}
