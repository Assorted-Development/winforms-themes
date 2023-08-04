using System.Diagnostics.CodeAnalysis;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// Implementation of a light theme. Depends on the default colors of forms
    /// </summary>
    public class DefaultLightTheme : AbstractTheme
    {
        public const string THEME_NAME = "LIGHT_DEFAULT_BUILTIN";

        public static readonly Color COLOR_BACK_ERROR = "#B00020".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY = "#EEEEEE".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_LIGHT = "#777777".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY = "#3487b2".ToColor();

        // Usually the form
        public static readonly Color COLOR_BACKGROUND = "#EEEEEE".ToColor();

        public static readonly Color COLOR_FORE_ERROR = "#FFFFFF".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#000000".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#ffffff".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#ffffff".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color COLOR_SURFACE = "#EEEEEE".ToColor();

        public static readonly Color COLOR_SURFACE_LIGHT = "#cccccc".ToColor();

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => COLOR_BACKGROUND;

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => COLOR_BACK_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => COLOR_FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => COLOR_SURFACE_LIGHT;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities => ThemeCapabilities.LightMode;

        [ExcludeFromCodeCoverage]
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