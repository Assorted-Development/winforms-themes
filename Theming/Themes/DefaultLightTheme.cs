using MFBot_1701_E.Utilities;
using System.Drawing;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// Implementation of a light theme. Depends on the default colors of forms
    /// </summary>
    //TODO: Currently, this does not switch back from dark mode as we depend on the colors of the forms
    //and just use this theme to colorize warnings and so on
    public class DefaultLightTheme : AbstractTheme
    {
        public const string THEME_NAME = "LIGHT_DEFAULT";

        public static readonly Color COLOR_BACK_PRIMARY = "#6200EE".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#FFFFFF".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY = "#03DAC6".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#000000".ToColor();
        public static readonly Color COLOR_BACK_SECONDARY_VARIANT = "#018786".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY_VARIANT = "#000000".ToColor();
        public static readonly Color COLOR_BACK_BACKGROUND = "#FFFFFF".ToColor();
        public static readonly Color COLOR_FORE_BACKGROUND = "#000000".ToColor();
        public static readonly Color COLOR_BACK_SURFACE = "#FFFFFF".ToColor();
        public static readonly Color COLOR_FORE_SURFACE = "#000000".ToColor();
        public static readonly Color COLOR_BACK_ERROR = "#B00020".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#FFFFFF".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.LightMode;

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
