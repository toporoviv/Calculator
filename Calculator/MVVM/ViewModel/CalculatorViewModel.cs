using Calculator.MVVM.Exceptions;
using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model;
using Calculator.MVVM.Model.Validator;
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
        private readonly Model.Calculator _calculator;
        private IExpressionBuilder _notation;
        private BaseTextBoxValidator _textBoxValidator;
        private readonly IExpressionValidator _expressionValidator;
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
            _expression = string.Empty;
            _expressionValidator = new ExpressionValidator();
            _textBoxValidator = new TextBoxValidator { FontSize = 20, TextBoxWidth = 280 };
        }

        public BaseTextBoxValidator TextBoxValidator
        {
            get => _textBoxValidator;
            set  
            {
                _textBoxValidator = value;
                OnPropertyChanged(nameof(TextBoxValidator));
            }
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
                catch(FormatException)
                {
                    MessageBox.Show("Введена некорректная строка");
                }
                catch (DivideByZeroException)
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
                try
                {
                    string symbol = obj as string;
                    _textBoxValidator.SymbolCount += symbol.Length;
                    Expression += symbol;
                }
                catch(ExpressionLengthOverflowException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        public ICommand ClearCommand
        {
            get => new Command(obj =>
            {
                Expression = "";
                _textBoxValidator.SymbolCount = 0;
            });
        }
    }
}
