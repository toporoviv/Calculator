using Calculator.MVVM.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model.Validator
{
    public abstract class BaseTextBoxValidator : BaseViewModel
    {
        private int _fontSize;
        private int _symbolCount;

        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (value > MaximumWidth) _fontSize = MaximumWidth;
                else _fontSize = Math.Max(MinimumWidth, value);

                OnPropertyChanged(nameof(FontSize));
            }
        }

        public int MaximumSymbolCount { get; set; } = 90;

        public int TextBoxWidth { get; set; }

        public int MinimumWidth { get; } = 10;

        public int MaximumWidth { get; } = 20;

        public int SymbolCount
        {
            get { return _symbolCount; }
            set
            {
                if (value > MaximumSymbolCount) throw new ExpressionLengthOverflowException("Превышен лимит ввода символов");
                _symbolCount = value;
                Recalculate();
            }
        }

        protected abstract void Recalculate();
    }
}
