using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Themes
{
    public class HighContrastDarkTheme : AbstractDarkTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

        protected override Color ControlBackColor => Color.DarkGray;
        protected override Color ControlForeColor => Color.LightGray;
    }
}
