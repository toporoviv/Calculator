using Calculator.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class RegexExpressionBuilder : IRegexExpressionBuilder
    {
        public string Build()
        {
            var result = string.Empty;

            var specialOperations = GetSpecialOperations();
            var operations = GetUsedOperations();

            for (int i = 0; i < operations.Count; i++)
            {
                if (specialOperations.Contains(operations[i])) result += $@"\{operations[i]}";
                else result += operations[i];
            }

            return result;
        }

        public List<string> GetSpecialOperations()
        {
            // все символы, которые зарезервированы в регулярных выражениях
            return new List<string>
            {
                "+", "-", ".", "*", "?", "^", "(", ")", "[", "]", "$", @"\", "{", "}", "|" 
            };
        }

        public List<string> GetUsedOperations()
        {
            return EngineeringCalculator.Operations.Keys.ToList();
        }
    }
}
