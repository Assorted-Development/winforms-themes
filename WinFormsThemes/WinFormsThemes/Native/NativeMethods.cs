using System.Runtime.InteropServices;

namespace WinFormsThemes.Native;

internal class NativeMethods
{
    /// <summary>
    /// constant to define dark mode option
    /// </summary>
    internal const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    /// constant to define dark mode option
    /// </summary>
    internal const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;

    /// <summary>
    /// native method to set the title bar style
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="attr"></param>
    /// <param name="attrValue"></param>
    /// <param name="attrSize"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    internal static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
}