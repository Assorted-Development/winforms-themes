using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Themes
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
        /// this theme has a high contrast
        /// </summary>
        HighContrast = 2
    }
}
