using System.Diagnostics.CodeAnalysis;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    public class HighContrastDarkTheme : AbstractTheme
    {
        public const string THEME_NAME = "DARK_HIGH_CONTRAST";

        public static readonly Color COLOR_BACK_ERROR = "#CF6679".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY = "#121212".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY = "#009a77".ToColor();

        // Usually the form
        public static readonly Color COLOR_BACKGROUND = "#121212".ToColor();

        public static readonly Color COLOR_FORE_ERROR = "#000000".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#FFFFFF".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#000000".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color COLOR_SURFACE = "#121212".ToColor();

        public static readonly Color COLOR_SURFACE_LIGHT = "#777777".ToColor();

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => COLOR_BACKGROUND;

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => COLOR_BACK_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => COLOR_FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => COLOR_SURFACE_LIGHT;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities => ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast;

        public override Color ControlBackColor => COLOR_SURFACE;

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorBackColor => COLOR_BACK_ERROR;

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorForeColor => COLOR_FORE_ERROR;

        [ExcludeFromCodeCoverage]
        public override Color ControlForeColor => COLOR_FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightColor => COLOR_BACK_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessBackColor => COLOR_BACK_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessForeColor => COLOR_FORE_SECONDARY;

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningBackColor => COLOR_BACK_PRIMARY_VARIANT;

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningForeColor => COLOR_FORE_PRIMARY_VARIANT;

        [ExcludeFromCodeCoverage]
        public override Color ForegroundColor => COLOR_FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override string Name => THEME_NAME;
    }
}