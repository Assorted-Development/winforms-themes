﻿using MFBot_1701_E.Utilities;
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
        public const string THEME_NAME = "LIGHT_DEFAULT_BUILTIN";

        public static readonly Color COLOR_BACK_PRIMARY = "#EEEEEE".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY = "#000000".ToColor();

        public static readonly Color COLOR_BACK_PRIMARY_VARIANT = "#3700B3".ToColor();
        public static readonly Color COLOR_FORE_PRIMARY_VARIANT = "#ffffff".ToColor();

        public static readonly Color COLOR_BACK_SECONDARY = "#3487b2".ToColor();
        public static readonly Color COLOR_FORE_SECONDARY = "#ffffff".ToColor();

        // Usually the form
        public static readonly Color COLOR_BACKGROUND = "#EEEEEE".ToColor();

        // usually the containers on the form (grid, tab controls, ..)
        public static readonly Color COLOR_SURFACE = "#EEEEEE".ToColor();
        public static readonly Color COLOR_SURFACE_LIGHT = "#777777".ToColor();

        public static readonly Color COLOR_BACK_ERROR = "#B00020".ToColor();
        public static readonly Color COLOR_FORE_ERROR = "#FFFFFF".ToColor();

        public override string Name => THEME_NAME;
        public override ThemeCapabilities Capabilities => ThemeCapabilities.LightMode;

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
