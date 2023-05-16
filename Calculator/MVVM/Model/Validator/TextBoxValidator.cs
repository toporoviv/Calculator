using Calculator.MVVM.Model.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.MVVM.Model
{
    public class TextBoxValidator : BaseTextBoxValidator
    {
        protected override void Recalculate()
        {
            if (SymbolCount == 0) return;

            double symbolWidth = Helper.GetSymbolWidth(FontSize);
            double currentWidth = TextBoxWidth / (double)SymbolCount;
            int newFontSize = FontSize;

            if (currentWidth > symbolWidth)
            {
                if (FontSize >= MaximumWidth) return;

                symbolWidth = Helper.GetSymbolWidth(++newFontSize);
                while (newFontSize <= MaximumWidth && symbolWidth < currentWidth) symbolWidth = Helper.GetSymbolWidth(++newFontSize);
            }
            else
            {
                if (FontSize <= MinimumWidth) return;

                symbolWidth = Helper.GetSymbolWidth(--newFontSize);
                while (newFontSize >= MinimumWidth && symbolWidth > currentWidth) symbolWidth = Helper.GetSymbolWidth(--newFontSize);
            }

            FontSize = newFontSize;
        }
    }
}
