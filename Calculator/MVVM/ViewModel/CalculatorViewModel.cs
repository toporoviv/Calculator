using Calculator.MVVM.Exceptions;
using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Calculator.MVVM.ViewModel
{
    public class CalculatorViewModel : BaseViewModel
    {
        private Model.Calculator _calculator;
        private IExpressionBuilder _notation;
        private IExpressionValidator _expressionValidator;
        private string _expression;

        public string Expression
        {
            get { return _expression; }
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public CalculatorViewModel()
        {
            _calculator = new Model.Calculator();
            _expressionValidator = new ExpressionValidator();
        }

        public ICommand EqualCommand
        {
            get => new Command(obj =>
            {
                try
                {
                    _notation = new PolishNotation(_expressionValidator, Expression);
                    Expression = _calculator.GetAnswer(_notation).ToString();
                }
                catch (OperationNotExistException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch(FormatException ex)
                {
                    MessageBox.Show("Введена некорректная строка");
                }
                catch (DivideByZeroException ex)
                {
                    MessageBox.Show("На 0 делить нельзя");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public ICommand AddDigitOrOperatorCommand
        {
            get => new Command(obj =>
            {
                string symbol = obj as string;
                Expression += symbol;
            });
        }

        public ICommand ClearCommand
        {
            get => new Command(obj => Expression = "");
        }
    }
}
