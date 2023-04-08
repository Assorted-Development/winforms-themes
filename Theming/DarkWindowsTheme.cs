using System;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// Utility class to also style the title bar (requires Windows 10+)
    /// </summary>
    public static class DarkWindowsTheme
    {
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
                var attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return NativeMethods.DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        public static bool UseDarkThemeVisualStyle(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                bool result = NativeMethods.SetWindowTheme(handle, enabled ? "DarkMode_Explorer" : null, null) == 0;

                // for some versions, an extra scrollbar hack is needed
                return result && NativeMethods.OpenThemeData(IntPtr.Zero, "Explorer::ScrollBar") != IntPtr.Zero;
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
            return Environment.OSVersion.Version.Major >= 10 &&
                   (Environment.OSVersion.Version.Major > 10 || Environment.OSVersion.Version.Build >= build);
        }
    }
}
