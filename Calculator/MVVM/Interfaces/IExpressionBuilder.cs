using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Interfaces
{
    public interface IExpressionBuilder
    {
        List<string> GetExpression();

        bool IsValid(Dictionary<string, int> operations);
    }
}
