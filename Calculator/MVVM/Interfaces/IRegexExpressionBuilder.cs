using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Interfaces
{
    public interface IRegexExpressionBuilder
    {
        List<string> GetUsedOperations();

        List<string> GetSpecialOperations();

        string Build();
    }
}
