namespace WinFormsThemes
{
    /// <summary>
    /// Holder of the central theme registry for applications without dependency injection and only a single theme
    /// </summary>
    public static class ThemeRegistryHolder
    {
        /// <summary>
        /// the current Theme
        /// </summary>
        public static IThemeRegistry? ThemeRegistry { get; internal set; }
    }
}