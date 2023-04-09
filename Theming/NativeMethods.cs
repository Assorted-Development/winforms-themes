﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MFBot_1701_E.Theming;

internal class NativeMethods
{
    [DllImport("user32.dll")]
    internal static extern int SendMessage(IntPtr wnd, int msg, bool param, int lparam);

    [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
    internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    internal static extern IntPtr OpenThemeData(IntPtr hWnd, String classList);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetWindowDC(IntPtr handle);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool IsWindowVisible(IntPtr hwnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern int GetClientRect(IntPtr hwnd, [In, Out] ref Rectangle rect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool InvalidateRect(IntPtr hwnd, ref Rectangle rect, bool bErase);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool ValidateRect(IntPtr hwnd, ref Rectangle rect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref Rectangle rect);

    /// <summary>
    /// native method to set the title bar style
    /// </summary>
    /// <param name="hwnd"></param>
    /// <param name="attr"></param>
    /// <param name="attrValue"></param>
    /// <param name="attrSize"></param>
    /// <returns></returns>
    [DllImport("dwmapi.dll")]
    internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
        ref int attrValue, int attrSize);
    
    /// <summary>
    /// constant to define dark mode option
    /// </summary>
    internal const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
    /// <summary>
    /// constant to define dark mode option
    /// </summary>
    internal const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    /// <summary>
    /// Sent to a window to allow changes in that window to be redrawn, or to prevent changes in that window from being redrawn
    /// </summary>
    internal const int WM_SETREDRAW = 11;

    public const int WM_PAINT = 0xF;

    /// <summary>
    /// Sent when the cursor is in an inactive window and the user presses a mouse button
    /// </summary>
    internal const uint WM_MOUSEACTIVATE = 0x21;
    /// <summary>
    /// Return value from <see cref="WM_MOUSEACTIVATE"/>: Activates the window, and does not discard the mouse message
    /// </summary>
    internal const uint MA_ACTIVATE = 1;
    /// <summary>
    /// Return value from <see cref="WM_MOUSEACTIVATE"/>: Activates the window, and discards the mouse message
    /// </summary>
    internal const uint MA_ACTIVATEANDEAT = 2;


    /*
     * GetWindow() Constants
     */
    public const int GW_HWNDFIRST = 0;
    public const int GW_HWNDLAST = 1;
    public const int GW_HWNDNEXT = 2;
    public const int GW_HWNDPREV = 3;
    public const int GW_OWNER = 4;
    public const int GW_CHILD = 5;
}