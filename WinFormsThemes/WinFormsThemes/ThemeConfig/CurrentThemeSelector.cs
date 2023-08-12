namespace WinFormsThemes.ThemeConfig
{
    /// <summary>
    /// A simple way to provide a current theme through <see cref="IThemeRegistry.Current"/>.
    /// This function is called everytime when <see cref="IThemeRegistry.Current"/> is accessed.
    /// </summary>
    /// <param name="registry">the source registry</param>
    /// <returns>the current theme</returns>
    public delegate ITheme? CurrentThemeSelector(IThemeRegistry registry);
}