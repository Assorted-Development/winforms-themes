using MFBot_1701_E.Utilities;
using System.Drawing;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// normal dark theme. For now, loosely based on the Material Design  Palette
    /// https://m2.material.io/design/color/dark-theme.html#ui-application
    /// Good source for picking colors in conjunction with other colors can be found on https://color.adobe.com/de/create/color-wheel
    /// </summary>
    public class DefaultDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_DEFAULT";

        public static readonly Color COLOR_BACK_PRIMARY = "#082a56".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_LIGHT = "#214d86".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#b7bfcf".ToColor();

        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#c8dfdf".ToColor();

        public static readonly Color COLOR_BACK_SECONDARY = "#38414B".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();

        // Usually the form
        public static readonly Color COLOR_BACKGROUND = "#191919".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color COLOR_SURFACE = "#191919".ToColor();
        public static readonly Color COLOR_SURFACE_LIGHT = "#202020".ToColor();

        public static readonly Color COLOR_BACK_ERROR = "#6688cf".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#000000".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode;

        protected override Color BackgroundColor => COLOR_BACKGROUND;
        protected override Color ForegroundColor => COLOR_FORE_PRIMARY;

        protected override Color ControlBackColor => COLOR_SURFACE;
        protected override Color ControlForeColor => COLOR_FORE_PRIMARY;
        protected override Color ControlHighlightColor => COLOR_BACK_SECONDARY;

        protected override Color ButtonBackColor => COLOR_BACK_PRIMARY;
        protected override Color ButtonForeColor => COLOR_FORE_PRIMARY;
        protected override Color ButtonHoverColor => COLOR_BACK_PRIMARY_LIGHT;

        protected override Color ControlSuccessBackColor => COLOR_BACK_SECONDARY;
        protected override Color ControlSuccessForeColor => COLOR_FORE_SECONDARY;
        protected override Color ControlWarningBackColor => COLOR_BACK_PRIMARY_VARIANT;
        protected override Color ControlWarningForeColor => COLOR_FORE_PRIMARY_VARIANT;
        protected override Color ControlErrorBackColor => COLOR_BACK_ERROR;
        protected override Color ControlErrorForeColor => COLOR_FORE_ERROR;
    }
}
