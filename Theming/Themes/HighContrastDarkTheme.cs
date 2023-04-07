using System.Drawing;
using MFBot_1701_E.Utilities;

namespace MFBot_1701_E.Theming.Themes
{
    public class HighContrastDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public static readonly Color COLOR_BACK_PRIMARY = "#001946".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#000000".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#000000".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY = "#009a77".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();
        public static readonly Color COLOR_BACK_BACKGROUND = "#121212".ToColor();
        public static readonly Color COLOR_FORE_BACKGROUND = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_SURFACE = "#121212".ToColor();
        public static readonly Color COLOR_FORE_SURFACE = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_ERROR = "#CF6679".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#000000".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

        protected override Color ControlBackColor => COLOR_BACK_BACKGROUND;
        protected override Color ControlForeColor => COLOR_FORE_BACKGROUND;
        protected override Color ControlHighlightColor => COLOR_BACK_SECONDARY;
        protected override Color ControlSuccessBackColor => COLOR_BACK_SECONDARY;
        protected override Color ControlSuccessForeColor => COLOR_FORE_SECONDARY;
        protected override Color ControlWarningBackColor => COLOR_BACK_PRIMARY_VARIANT;
        protected override Color ControlWarningForeColor => COLOR_FORE_PRIMARY_VARIANT;
        protected override Color ControlErrorBackColor => COLOR_BACK_ERROR;
        protected override Color ControlErrorForeColor => COLOR_FORE_ERROR;
    }
}
