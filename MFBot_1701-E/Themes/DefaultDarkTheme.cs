using MFBot_1701_E.Utilities;
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
    /// normal dark theme. For now, based on the Material Design  Palette
    /// https://m2.material.io/design/color/dark-theme.html#ui-application
    /// Primary             : #BB86FC
    /// Forecolor Primary   : #000000
    /// Primary Variant     : #3700B3
    /// Forecolor Prim Var  : #000000
    /// Secondary           : #03DAC6
    /// Forecolor Secondary : #000000
    /// Background          : #FFFFFF
    /// Forecolor Background: #000000
    /// Surface             : #121212
    /// Forecolor Surface   : #FFFFFF
    /// Error               : #CF6679
    /// Forecolor Error     : #000000
    /// </summary>
    public class DefaultDarkTheme : AbstractDarkTheme
    {
        public const string THEME_NAME = "DARK_DEFAULT";

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode;

        protected override Color ControlBackColor => "#121212".ToColor();
        protected override Color ControlForeColor => "#FFFFFF".ToColor();
        protected override Color ControlSuccessBackColor => "#03DAC6".ToColor();
        protected override Color ControlSuccessForeColor => ControlBackColor;
        protected override Color ControlWarningBackColor => "#3700B3".ToColor();
        protected override Color ControlWarningForeColor => ControlForeColor;
        protected override Color ControlErrorBackColor => "#CF6679".ToColor();
        protected override Color ControlErrorForeColor => ControlBackColor;
    }
}
