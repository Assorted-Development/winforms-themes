using Microsoft.Win32;

namespace WinFormsThemes.Utilities
{
    /// <summary>
    /// Utilities to detect high contrast and dark mode
    /// </summary>
    internal static class WindowsThemeDetector
    {
        /// <summary>
        /// retruns true if the global dark mode is enabled
        /// </summary>
        /// <returns></returns>
        internal static bool GetDarkMode()
        {
            Object? regValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1);
            if (regValue == null)
            {
                return false;
            }
            else
            {
                return (int)regValue == 0;
            }
        }

        /// <summary>
        /// returns true if the global high contrast is enabled
        /// </summary>
        /// <returns></returns>
        internal static bool GetHighContrast()
        {
            return SystemInformation.HighContrast;
        }
    }
}