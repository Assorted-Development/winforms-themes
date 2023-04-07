using System;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// features of a given theme
    /// </summary>
    [Flags]
    public enum ThemeCapabilities
    {
        None = 0,
        /// <summary>
        /// this theme is a dark theme
        /// </summary>
        DarkMode = 1,
        /// <summary>
        /// this theme is a light theme
        /// </summary>
        LightMode = 2,
        /// <summary>
        /// this theme has a high contrast
        /// </summary>
        HighContrast = 3
    }
}
