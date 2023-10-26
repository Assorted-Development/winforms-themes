namespace WinFormsThemes.ThemeConfig
{
    /// <summary>
    /// A simple way to provide a current theme through <see cref="IThemeRegistry.CurrentTheme"/>.
    /// This function is called everytime when <see cref="IThemeRegistry.CurrentTheme"/> is accessed.
    /// </summary>
    /// <param name="registry">the source registry</param>
    /// <returns>the current theme</returns>
    public delegate ITheme? CurrentThemeSelector(IThemeRegistry registry);
}