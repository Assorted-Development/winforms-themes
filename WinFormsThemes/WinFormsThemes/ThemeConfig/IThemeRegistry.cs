namespace WinFormsThemes
{
    /// <summary>
    /// interface for interacting with the theme registry
    /// </summary>
    public interface IThemeRegistry
    {
        /// <summary>
        /// Event triggers on Theme Change
        /// </summary>
        public event EventHandler? OnThemeChanged;

        /// <summary>
        /// the builder to create a new IThemeRegistry
        /// </summary>
        public static IThemeRegistryBuilder BUILDER => new ThemeRegistryBuilder();

        /// <summary>
        /// the current theme
        /// </summary>
        ITheme? Current { get; set; }

        /// <summary>
        /// return the theme capabilities as configured by the user
        /// </summary>
        /// <returns></returns>
        ITheme? Get();

        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ITheme? Get(string name);

        /// <summary>
        /// returns a theme with the matching capabilities
        /// </summary>
        /// <param name="caps"></param>
        /// <param name="advancedCapabilitiesFilters"></param>
        /// <returns></returns>
        ITheme? Get(ThemeCapabilities caps, params string[] advancedCapabilitiesFilters);

        /// <summary>
        /// returns all themes
        /// </summary>
        /// <returns></returns>
        List<ITheme> List();

        /// <summary>
        /// returns the list of all theme names
        /// </summary>
        /// <returns></returns>
        List<string> ListNames();
    }
}