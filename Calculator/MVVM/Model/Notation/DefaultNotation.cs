using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculator.MVVM.Model.Notation
{
    public class DefaultNotation : IExpression
    {
        public List<string> GetExpression(string expression)
        {
            expression = expression
                .NormalizedExpression()
                .EnsureIsNotNullAndNotEmpty()
                .EnsureScopesArePlacedCorrectly()
                .EnsureNumberOfOperandsAndOperationsIsCorrect();

            var matches = Regex.Matches(expression, RegexHelper.Pattern);

            var expressionResult = new List<string>();

            for (int i = 0; i < matches.Count; i++)
            {
                expressionResult.Add(matches[i].Value);
            }

            return expressionResult;
        }
    }
}
