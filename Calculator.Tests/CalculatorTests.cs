using Calculator.MVVM.Model;
using Calculator.MVVM.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void GetAnswer_SimplyExpression_ShouldReturnCorrectResult()
        {
            string expression = "1 + 2";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(3).Within(0.001));
        }

        [Test]
        public void GetAnswer_SecondSimplyExpression_ShouldReturnCorrectResult()
        {
            string expression = "1 + 2.5 - 1.50";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(2).Within(0.001));
        }

        [Test]
        public void GetAnswer_ComplexExpression_ShouldReturnCorrectResult()
        {
            string expression = "1 - (2 * (3 - 2.5)) + 1 / 4";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(0.25).Within(0.001));
        }

        [Test]
        public void GetAnswer_SecondComplexExpression_ShouldReturnCorrectResult()
        {
            string expression = "(5 / 9 * 9 / 5 - 10 / 9) * 100 / (1 / 9)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(-100).Within(0.001));
        }

        [Test]
        public void GetAnswer_InvalidExpression_DivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                string expression = "1 / (5.000005 - 5.000006)";

                var notation = new PolishNotation(new ExpressionValidator(), expression);

                var calculator = new Calculator.MVVM.Model.Calculator();

                var result = calculator.GetAnswer(notation);
            });
        }

        [Test]
        public void GetAnswer_ThirdComplexExpression_ShoudlReturnCorrectResult()
        {
            Assert.Throws<DivideByZeroException>(() =>
            {
                string expression = "(1 / 3 - 1 / 3) / ((5 / 4) * 20 / 25 - 30 / 9 * 3 / 10)";

                var notation = new PolishNotation(new ExpressionValidator(), expression);

                var calculator = new Calculator.MVVM.Model.Calculator();

                var result = calculator.GetAnswer(notation);
            });
        }

        [Test]
        public void GetAnswer_SimplyValidExpressionWithUnaryMinus_ShouldReturnCorrectResult()
        {
            string expression = "4 / (-2)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(-2).Within(0.001));
        }

        [Test]
        public void GetAnswer_ComplexValidExpressionWithUnaryMinus_ShouldReturnCorrectResult()
        {
            string expression = "-5.5 - -2.5 + (-0.23 - -((23)) / -4.2)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(-8.706).Within(0.001));
        }

        [Test]
        public void GetAnswer_SimplyValidExpressionWithPowOperation_ShouldReturnCorrectResult()
        {
            string expression = "2 ^ 2 + 2 ^ 3";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(12).Within(0.001));
        }

        [Test]
        public void GetAnswer_ValidExpressionWithPowOperation_ShouldReturnCorrectResult()
        {
            string expression = "(2^0 + 2 ^ -1)^2";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(2.25).Within(0.001));
        }

        [Test]
        public void GetAnswer_ComplexValidExpressionWithPowOperation_ShouldReturnCorrectResult()
        {
            string expression = "2 ^ (3 / 4 - (1 / 2.5^2)) + 1 / (2 ^ -4)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var calculator = new Calculator.MVVM.Model.Calculator();

            var result = calculator.GetAnswer(notation);

            Assert.That(result, Is.EqualTo(17.505).Within(0.001));
        }
    }
}
