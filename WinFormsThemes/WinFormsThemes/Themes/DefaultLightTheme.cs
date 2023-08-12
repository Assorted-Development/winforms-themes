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

        public static readonly Color BACK_ERROR = "#B00020".ToColor();
        public static readonly Color BACK_PRIMARY = "#EEEEEE".ToColor();
        public static readonly Color BACK_PRIMARY_LIGHT = "#777777".ToColor();
        public static readonly Color BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color BACK_SECONDARY = "#3487b2".ToColor();

        // Usually the form
        public static readonly Color BACKGROUND = "#EEEEEE".ToColor();

        public static readonly Color FORE_ERROR = "#FFFFFF".ToColor();
        public static readonly Color FORE_PRIMARY = "#000000".ToColor();
        public static readonly Color FORE_PRIMARY_VARIANT = "#ffffff".ToColor();
        public static readonly Color FORE_SECONDARY = "#ffffff".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color SURFACE = "#EEEEEE".ToColor();

        public static readonly Color SURFACE_LIGHT = "#cccccc".ToColor();

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => BACKGROUND;

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => BACK_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => FORE_PRIMARY;

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => SURFACE_LIGHT;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities => ThemeCapabilities.LightMode;

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