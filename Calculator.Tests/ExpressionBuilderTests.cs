using Calculator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestFixture]
    public class ExpressionBuilderTests
    {
        [Test]
        public void GetExpression_SimpleExpression_ShouldProduceCorrectPolishNotation()
        {
            string expression = "1 + 2";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 1 + 2 +"));
        }

        [Test]
        public void GetExpression_SecondSimpleExpression_ShouldProduceCorrectPolishNotation()
        {
            string expression = "1 + 2 - 3";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 1 + 2 + 3 -"));
        }

        [Test]
        public void GetExpression_ComplexExpression_ShouldProduceCorrectPolishNotation()
        {
            var expression = "(2 - 2.5) * 3 / ((4 + 2.32) - 1)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 2 2.5 - 3 * 4 2.32 + 1 - / +"));
        }

        [Test]
        public void GetExpression_SecondComplexExpression_ShouldProduceCorrectPolishNotation()
        {
            var expression = "3 * (0 - 2.5 * 1.32) / ((5)) + 2.3";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 3 0 2.5 1.32 * - * 5 / 2.3 + +"));
        }

        [Test]
        public void IsValid_EmptyString_ShouldReturnFalse()
        {
            string expression = string.Empty;

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_ValidExpression_ShouldReturnTrue()
        {
            string expression = "(3 + 2) - 1 * 4 - (5 - 2 / 3)";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_ValidExpression_ShouldReturnFalse()
        {
            string expression = "3 - 2 + ((1 * 4))";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_ComplexValidExpression_ShouldReturnTrue()
        {
            string expression = "3.33 - 2.13 + ((1.002 * 4.25) - 1 / (2.5 - 5.3))";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_SecondInvalidExpression_ShouldReturnFalse()
        {
            string expression = "1 - 2 + ( * ) - 3";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_ThirdInvalidExpression_ShouldReturnFalse()
        {
            string expression = "1.25 - 2.32o + (53.t35) - 3.005";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_InvalidExpressionWithUnknownSymbols_ShouldReturnFalse()
        {
            string expression = "x - ( + t * 3))";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_ValidExpressionWithUnknownSymbols_ShouldReturnFalse()
        {
            string expression = "x + y - t / x";

            var notation = new PolishNotation(new ExpressionValidator(), expression);

            var result = notation.IsValid();

            Assert.That(result, Is.EqualTo(false));
        }
    }
}
