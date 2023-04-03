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
    /// </summary>
    public class DefaultDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_DEFAULT";

        public static readonly Color COLOR_BACK_PRIMARY = "#BB86FC".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#000000".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#000000".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY = "#03DAC6".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();
        public static readonly Color COLOR_BACK_BACKGROUND = "#121212".ToColor();
        public static readonly Color COLOR_FORE_BACKGROUND = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_SURFACE = "#121212".ToColor();
        public static readonly Color COLOR_FORE_SURFACE = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_ERROR = "#CF6679".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#000000".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode;

        protected override Color ControlBackColor => COLOR_BACK_BACKGROUND;
        protected override Color ControlForeColor => COLOR_FORE_BACKGROUND;
        protected override Color ControlSuccessBackColor => COLOR_BACK_SECONDARY;
        protected override Color ControlSuccessForeColor => COLOR_FORE_SECONDARY;
        protected override Color ControlWarningBackColor => COLOR_BACK_PRIMARY_VARIANT;
        protected override Color ControlWarningForeColor => COLOR_FORE_PRIMARY_VARIANT;
        protected override Color ControlErrorBackColor => COLOR_BACK_ERROR;
        protected override Color ControlErrorForeColor => COLOR_FORE_ERROR;
        protected override Color ControlHintForeColor => Color.FromArgb((int)(255 * 0.6), ControlForeColor);
    }
}
