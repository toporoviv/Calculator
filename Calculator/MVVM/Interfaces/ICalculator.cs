using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Interfaces
{
    public interface ICalculator
    {
        double GetAnswer(IExpression notation, string expression);

        bool IsOperator(string operation);
    }
}
