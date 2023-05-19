namespace Calculator.MVVM.Model.Factory.CalculatorFactory
{
    public class GeneralCalculatorFactory : CalculatorFactory
    {
        public override BaseCalculator CreateCalculator()
        {
            return new GeneralCalculator();
        }
    }
}
