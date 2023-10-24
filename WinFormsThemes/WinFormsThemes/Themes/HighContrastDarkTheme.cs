using System.Diagnostics.CodeAnalysis;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    public class HighContrastDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public static readonly Color BACK_ERROR = "#CF6679".ToColor();
        public static readonly Color BACK_PRIMARY = "#121212".ToColor();
        public static readonly Color BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color BACK_SECONDARY = "#009a77".ToColor();

        /// <summary>
        /// Background color, this is usually the form background
        /// </summary>
        public static readonly Color BACKGROUND = "#121212".ToColor();

        public static readonly Color FORE_ERROR = "#000000".ToColor();
        public static readonly Color FORE_PRIMARY = "#FFFFFF".ToColor();
        public static readonly Color FORE_PRIMARY_VARIANT = "#000000".ToColor();
        public static readonly Color FORE_SECONDARY = "#000000".ToColor();

        /// <summary>
        /// Surface color, this is usually the one for containers on the form (grid, tab controls, ..)
        /// </summary>
        public static readonly Color SURFACE = "#121212".ToColor();

        public static readonly Color SURFACE_LIGHT = "#777777".ToColor();

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => BACKGROUND;

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => BACK_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => SURFACE_LIGHT;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

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
