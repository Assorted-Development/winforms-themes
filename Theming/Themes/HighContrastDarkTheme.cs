using System.Drawing;
using MFBot_1701_E.Utilities;

namespace MFBot_1701_E.Theming.Themes
{
    public class HighContrastDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public static readonly Color COLOR_BACK_PRIMARY = "#121212".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#FFFFFF".ToColor();

        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#000000".ToColor();

        public static readonly Color COLOR_BACK_SECONDARY = "#009a77".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();

        // Usually the form
        public static readonly Color COLOR_BACKGROUND = "#121212".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color COLOR_SURFACE = "#121212".ToColor();
        public static readonly Color COLOR_SURFACE_LIGHT = "#777777".ToColor();

        public static readonly Color COLOR_BACK_ERROR = "#CF6679".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#000000".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

        protected override Color BackgroundColor => COLOR_BACKGROUND;
        protected override Color ForegroundColor => COLOR_FORE_PRIMARY;

        protected override Color ControlBackColor => COLOR_SURFACE;
        protected override Color ControlForeColor => COLOR_FORE_PRIMARY;
        protected override Color ControlHighlightColor => COLOR_BACK_SECONDARY;

        protected override Color ButtonBackColor => COLOR_BACK_PRIMARY;
        protected override Color ButtonForeColor => COLOR_FORE_PRIMARY;
        protected override Color ButtonHoverColor => COLOR_SURFACE_LIGHT;

        protected override Color ControlSuccessBackColor => COLOR_BACK_SECONDARY;
        protected override Color ControlSuccessForeColor => COLOR_FORE_SECONDARY;
        protected override Color ControlWarningBackColor => COLOR_BACK_PRIMARY_VARIANT;
        protected override Color ControlWarningForeColor => COLOR_FORE_PRIMARY_VARIANT;
        protected override Color ControlErrorBackColor => COLOR_BACK_ERROR;
        protected override Color ControlErrorForeColor => COLOR_FORE_ERROR;
    }
}
