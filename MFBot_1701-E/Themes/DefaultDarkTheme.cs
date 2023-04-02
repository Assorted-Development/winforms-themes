using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFBot_1701_E.Themes
{
    /// <summary>
    /// normal dark theme
    /// </summary>
    public class DefaultDarkTheme : AbstractDarkTheme
    {
        public const string THEME_NAME = "DARK_DEFAULT";

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode;

        protected override Color ControlBackColor => Color.DarkGray;
        protected override Color ControlForeColor => Color.LightGray;
    }
}
