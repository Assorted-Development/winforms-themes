using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Theming.Themes
{
    public class HighContrastDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

        protected override Color ControlBackColor => Color.DarkGray;
        protected override Color ControlForeColor => Color.LightGray;
        protected override Color ControlSuccessBackColor => ControlBackColor;
        protected override Color ControlSuccessForeColor => ControlForeColor;
        protected override Color ControlWarningBackColor => ControlBackColor;
        protected override Color ControlWarningForeColor => ControlForeColor;
        protected override Color ControlErrorBackColor => ControlBackColor;
        protected override Color ControlErrorForeColor => ControlForeColor;
    }
}
