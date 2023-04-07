using System.Windows.Forms;
using Microsoft.Win32;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// Utilities to detect high contrast and dark mode
    /// </summary>
    public static class WindowsThemeDetector
    {
        /// <summary>
        /// retruns true if the global dark mode is enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetDarkMode()
        {
            return ((int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1)) == 0;
        }
        /// <summary>
        /// returns true if the global high contrast is enabled
        /// </summary>
        /// <returns></returns>
        public static bool GetHighContrast()
        {
            return SystemInformation.HighContrast;
        }
    }
}
