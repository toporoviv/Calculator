using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Exceptions
{
    public class NumberOfOperandsAndOperationsException : Exception
    {
        public NumberOfOperandsAndOperationsException(string message) : base(message) 
        { 
        }
    }
}
