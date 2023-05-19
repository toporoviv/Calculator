using Calculator.MVVM.Model.Enum;
using Calculator.MVVM.Model.Factory;
using System.Collections.Generic;

namespace Calculator.MVVM.Model.Helper
{
    public static class Helper
    {
        public static double GetSymbolWidth(int fontSize) => 0.65 * fontSize - 0.29;
    }
}
