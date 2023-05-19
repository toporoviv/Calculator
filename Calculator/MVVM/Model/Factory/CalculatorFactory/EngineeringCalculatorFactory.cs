using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Factory.CalculatorFactory
{
    public class EngineeringCalculatorFactory : CalculatorFactory
    {
        public override BaseCalculator CreateCalculator()
        {
            return new EngineeringCalculator();
        }
    }
}
