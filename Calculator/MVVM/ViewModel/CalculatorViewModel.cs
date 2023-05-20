using Calculator.MVVM.Exceptions;
using Calculator.MVVM.Interfaces;
using Calculator.MVVM.Model;
using Calculator.MVVM.Model.Enum;
using Calculator.MVVM.Model.Factory.CalculatorFactory;
using Calculator.MVVM.Model.Factory.ExpressionFactory;
using Calculator.MVVM.Model.Helper;
using Calculator.MVVM.Model.Notation;
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
        private BaseCalculator _calculator;
        private IExpression _notation;
        private CalculatorEnum _currentCalculator;
        private BaseTextBoxValidator _textBoxValidator;
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
            _calculator = new GeneralCalculator();
            _expression = string.Empty;
            _currentCalculator = CalculatorEnum.GeneralCalculator;
            _textBoxValidator = new TextBoxValidator { FontSize = 20, TextBoxWidth = 295 };
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

        public Visibility IsGeneralCalculator
        {
            get => _currentCalculator == CalculatorEnum.GeneralCalculator ? Visibility.Visible : Visibility.Hidden;
        }

        public Visibility IsEngineeringCalculator
        {
            get => _currentCalculator == CalculatorEnum.EngineeringCalculator ? Visibility.Visible : Visibility.Hidden;
        }

        public ICommand EqualCommand
        {
            get => new Command(obj =>
            {
                try
                {
                    var expressionFactory = new ExpressionFactory();
                    _notation = expressionFactory.CreateExpression(_currentCalculator);
                    Expression = _calculator.GetAnswer(_notation, _expression).ToString();
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

        public ICommand ChangeCalculatorCommand
        {
            get => new Command(obj =>
            {
                Expression = "";
                _textBoxValidator.SymbolCount = 0;
                _currentCalculator = (CalculatorEnum)obj;
                var producer = new CalculatorProducer(_currentCalculator);

                try
                {
                    _calculator = producer.GetFactory().CreateCalculator();
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                OnPropertyChanged(nameof(IsGeneralCalculator));
                OnPropertyChanged(nameof(IsEngineeringCalculator));
            });
        }
    }
}
