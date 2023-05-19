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
    public class DefaultNotationTests
    {
        [Test]
        public void GetExpression_SimpleExpression_ShouldProduceCorrectDefaultNotation()
        {
            var expression = "1 + 2 * 3";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 + " + expression));
        }

        [Test]
        public void GetExpression_SecondSimpleExpression_ShouldProduceCorrectDefaultNotation()
        {
            var expression = "4 - 2 + 3 * 1.5";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 + " + expression));
        }

        [Test]
        public void GetExpression_ComplexExpression_ShouldProduceCorrectDefaultNotation()
        {
            var expression = "5 - 2.5 + 0.3 - 52.23 * 6 / 4 / 2.5 * 2.3";

            var notation = new DefaultNotation(new ExpressionValidator(), expression);

            var result = string.Join(" ", notation.GetExpression());

            Assert.That(result, Is.EqualTo("0 + " + expression));
        }
    }
}
