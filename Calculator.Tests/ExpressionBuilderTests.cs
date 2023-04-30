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

            var notation = new PolishNotation(expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("+ 1 2"));
        }

        [Test]
        public void GetExpression_SecondSimpleExpression_ShouldProduceCorrectPolishNotation()
        {
            string expression = "1 + 2 - 3";

            var notation = new PolishNotation(expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("+ 1 - 2 3"));
        }

        [Test]
        public void GetExpression_ComplexExpression_ShouldProduceCorrectPolishNotation()
        {
            var expression = "(2 - 2.5) * 3 / ((4 + 2.32) - 1))";

            var notation = new PolishNotation(expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("+ 1 - 2 3"));
        }

        [Test]
        public void IsValid_EmptyString_ShouldReturnTrue()
        {
            string expression = string.Empty;

            var notation = new PolishNotation(expression);

            var result = notation.IsValid(Calculator.MVVM.Model.Calculator._operations);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_ValidExpression_ShouldReturnTrue()
        {
            string expression = "(3 + 2) - 1 * 4 - (5 - 2 / 3)";

            var notation = new PolishNotation(expression);

            var result = notation.IsValid(Calculator.MVVM.Model.Calculator._operations);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsValid_InvalidExpression_ShouldReturnFalse()
        {
            string expression = "1 - 2 + ( * ) - 3";

            var notation = new PolishNotation(expression);

            var result = notation.IsValid(Calculator.MVVM.Model.Calculator._operations);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_InvalidExpressionWithUnknownSymbols_ShouldReturnFalse()
        {
            string expression = "x - ( + t * 3))";

            var notation = new PolishNotation(expression);

            var result = notation.IsValid(Calculator.MVVM.Model.Calculator._operations);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsValid_ValidExpressionWithUnknownSymbols_ShouldReturnFalse()
        {
            string expression = "x + y - t / x";

            var notation = new PolishNotation(expression);

            var result = notation.IsValid(Calculator.MVVM.Model.Calculator._operations);

            Assert.That(result, Is.EqualTo(false));
        }
    }
}
