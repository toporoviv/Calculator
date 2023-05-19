using Calculator.MVVM.Exceptions;

namespace Calculator.MVVM.Model.Helper
{
    public static class OperationHelper
    {
        public static int GetOperationPriority(string operation)
        {
            if (BaseCalculator.Operations.ContainsKey(operation)) return BaseCalculator.Operations[operation];
            else throw new OperationNotExistException($"операции {operation} не существует");
        }
    }
}
