using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public static class Helper
    {
        public static double GetSymbolWidth(int fontSize) => 0.65 * fontSize - 0.29;
    }
}
