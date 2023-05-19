using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model.Enum;
using Calculator.MVVM.Model.Notation;
using Calculator.MVVM.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Factory.ExpressionFactory
{
    public class ExpressionFactory : IExpressionFactory
    {
        public IExpression CreateExpression(CalculatorEnum calculatorEnum, string expression)
        {
            switch (calculatorEnum)
            {
                case CalculatorEnum.GeneralCalculator: return new DefaultNotation(new ExpressionValidator(), expression);
                case CalculatorEnum.EngineeringCalculator: return new PolishNotation(new ExpressionValidator(), expression);
                default: return null;
            }
        }
    }
}
