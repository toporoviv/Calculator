using Calculator.MVVM.Model.Notation;
using Calculator.MVVM.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestFixture]
    public class GeneralCalculatorTests
    {
        [Test]
        public void GetAnswer_SimpleExpression_ShouldReturnCorrectResult()
        {
            string expression = "1 + 2 * 3";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.GeneralCalculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(9).Within(0.001));
        }

        [Test]
        public void GetAnswer_SecondSimpleExpression_ShouldReturnCorrectResult()
        {
            string expression = "1.5 - 0.5 + 2.5 * 8 / 4";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.GeneralCalculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(7).Within(0.001));
        }

        [Test]
        public void GetAnswer_ComplexExpression_ShouldReturnCorrectResult()
        {
            string expression = "4 / 2 - 2 + 2.5 / 0.5 - 1.5 / 5";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.GeneralCalculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(0.7).Within(0.001));
        }
    }
}
