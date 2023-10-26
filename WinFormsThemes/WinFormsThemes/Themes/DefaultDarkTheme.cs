using System.Diagnostics.CodeAnalysis;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// normal dark theme. For now, loosely based on the Material Design  Palette
    /// https://m2.material.io/design/color/dark-theme.html#ui-application
    /// Good source for picking colors in conjunction with other colors can be found on https://color.adobe.com/de/create/color-wheel
    /// </summary>
    public class DefaultDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_DEFAULT";

        public static readonly Color BACK_ERROR = "#6688cf".ToColor();
        public static readonly Color BACK_PRIMARY = "#082a56".ToColor();
        public static readonly Color BACK_PRIMARY_LIGHT = "#214d86".ToColor();
        public static readonly Color BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color BACK_SECONDARY = "#38414B".ToColor();

        /// <summary>
        /// Background color, this is usually the form background
        /// </summary>
        public static readonly Color BACKGROUND = "#191919".ToColor();

        public static readonly Color FORE_ERROR = "#000000".ToColor();
        public static readonly Color FORE_PRIMARY = "#b7bfcf".ToColor();
        public static readonly Color FORE_PRIMARY_VARIANT = "#c8dfdf".ToColor();
        public static readonly Color FORE_SECONDARY = "#000000".ToColor();

        /// <summary>
        /// Surface color, this is usually the one for containers on the form (grid, tab controls, ..)
        /// </summary>
        public static readonly Color SURFACE = "#191919".ToColor();

        public static readonly Color SURFACE_LIGHT = "#202020".ToColor();

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => BACKGROUND;

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => BACK_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => BACK_PRIMARY_LIGHT;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode;

        [ExcludeFromCodeCoverage]
        public override Color ControlBackColor => SURFACE;

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorBackColor => BACK_ERROR;

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorForeColor => FORE_ERROR;

        [ExcludeFromCodeCoverage]
        public override Color ControlForeColor => FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightColor => BACK_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessBackColor => BACK_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessForeColor => FORE_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningBackColor => BACK_PRIMARY_VARIANT;

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningForeColor => FORE_PRIMARY_VARIANT;

        [ExcludeFromCodeCoverage]
        public override Color ForegroundColor => FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override string Name => THEME_NAME;
    }
}
