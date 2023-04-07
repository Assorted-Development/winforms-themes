using System;
using System.Runtime.InteropServices;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// Utility class to also style the title bar (requires Windows 10+)
    /// </summary>
    public static class DarkTitleBar
    {
        /// <summary>
        /// native method to set the title bar style
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <param name="attrSize"></param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
        ref int attrValue, int attrSize);
        /// <summary>
        /// constant to define dark mode option
        /// </summary>
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        /// <summary>
        /// constant to define dark mode option
        /// </summary>
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        /// <summary>
        /// enable/disable dark mode for the title bar
        /// </summary>
        /// <param name="handle">the Form handle</param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }
        /// <summary>
        /// returns true if we are running on Windows 10 or later
        /// </summary>
        /// <param name="build"></param>
        /// <returns></returns>
        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
