using Calculator.MVVM.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Factory.CalculatorFactory
{
    public class CalculatorProducer
    {
        private CalculatorEnum _calculatorEnum;

        public CalculatorProducer(CalculatorEnum calculatorEnum)
        {
            _calculatorEnum = calculatorEnum;
        }

        public CalculatorFactory GetFactory()
        {
            switch (_calculatorEnum)
            {
                case CalculatorEnum.GeneralCalculator:
                    return new GeneralCalculatorFactory();
                case CalculatorEnum.EngineeringCalculator:
                    return new EngineeringCalculatorFactory();
                default: return null;
            }
        }
    }
}
