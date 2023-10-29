using WinFormsThemes.Native;

namespace WinFormsThemes.Utilities
{
    /// <summary>
    /// Utility class to also style the title bar (requires Windows 10+)
    /// </summary>
    internal static class DarkWindowsTheme
    {
        /// <summary>
        /// returns true if we are running on Windows 10 or later
        /// </summary>
        /// <param name="build"></param>
        internal static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 &&
                   (Environment.OSVersion.Version.Major > 10 || Environment.OSVersion.Version.Build >= build);
        }

        internal static bool UseDarkThemeVisualStyle(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
#pragma warning disable CS8604, CS8625 // Mögliches Nullverweisargument.
                bool result = NativeMethods.SetWindowTheme(handle, enabled ? "DarkMode_Explorer" : null, null) == 0;
#pragma warning restore CS8604, CS8625 // Mögliches Nullverweisargument.

                // for some versions, an extra scrollbar hack is needed
                return result && NativeMethods.OpenThemeData(IntPtr.Zero, "Explorer::ScrollBar") != IntPtr.Zero;
            }

            return false;
        }

        /// <summary>
        /// enable/disable dark mode for the title bar
        /// </summary>
        /// <param name="handle">the Form handle</param>
        /// <param name="enabled"></param>
        internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                int attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return NativeMethods.DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }
    }
}