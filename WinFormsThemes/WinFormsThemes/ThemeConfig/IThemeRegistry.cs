using WinFormsThemes.ThemeConfig;

namespace WinFormsThemes
{
    /// <summary>
    /// interface for interacting with the theme registry
    /// </summary>
    public interface IThemeRegistry
    {
        /// <summary>
        /// Event that triggers when the <see cref="CurrentTheme"/> theme has changed
        /// </summary>
        public event EventHandler? OnThemeChanged;

        /// <summary>
        /// the current theme
        /// </summary>
        /// <exception cref="InvalidOperationException">thrown when <see cref="IThemeRegistryBuilder.WithCurrentThemeSelector(CurrentThemeSelector)"/> was not called</exception>
        ITheme? CurrentTheme { get; }

        /// <summary>
        /// returns the theme with capabilities configured by the user
        /// </summary>
        /// <returns></returns>
        ITheme? GetTheme();

        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ITheme? GetTheme(string name);

        /// <summary>
        /// returns a theme with the matching capabilities
        /// </summary>
        /// <param name="caps">Can be used to filter for light/dark/high contrast themes</param>
        /// <param name="advancedCapabilitiesFilters">Can be used to filter for custom identifiers in custom themes (e.g. file based themes)</param>
        /// <returns></returns>
        ITheme? GetTheme(ThemeCapabilities caps, params string[] advancedCapabilitiesFilters);

        /// <summary>
        /// returns all themes that are registered in this <see cref="IThemeRegistry"/> instance
        /// </summary>
        IList<ITheme> ListThemes();

        /// <summary>
        /// returns a list of the names of all themes that are registered in this <see cref="IThemeRegistry"/> instance
        /// </summary>
        IList<string> ListNames();
    }
}
