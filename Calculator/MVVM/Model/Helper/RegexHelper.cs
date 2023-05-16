using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public static class RegexHelper
    {
        public static string Pattern { get; } = @"(\d+\.\d+)|(\d+)";
        public static string PatternForReplace { get; } = @"([\D\)]-\d+\.\d+)|([\D\)]-\d+)";

        static RegexHelper()
        {
            var builder = new RegexExpressionBuilder();

            var result = builder.Build();

            Pattern += $@"|([{result}])";
        }
    }
}
