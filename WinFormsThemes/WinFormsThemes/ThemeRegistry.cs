using System.Collections.ObjectModel;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// registry for all Themes
    /// </summary>
    public static class ThemeRegistry
    {
        /// <summary>
        /// Event triggers on Theme Change
        /// </summary>
        public static event EventHandler OnThemeChanged;
        /// <summary>
        /// the current theme
        /// </summary>
        private static ITheme? _current = null;
        /// <summary>
        /// all objects to detect existing themes
        /// </summary>
        private static readonly List<IThemeLookup> _themeLookups = new()
        {
            new ConstantThemeLookup(),
            new FileThemeLookup(),
            new ResourceThemeLookup()
        };
        /// <summary>
        /// the current theme
        /// </summary>
        public static ITheme Current
        {
            get;
            set;
        } = GetTheme();
        /// <summary>
        /// return the theme capabilities
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static ThemeCapabilities GetThemeCaps(bool dark, bool highContrast)
        {
            ThemeCapabilities caps = ThemeCapabilities.None;
            if (dark)
            {
                caps |= ThemeCapabilities.DarkMode;
            }
            else
            {
                caps |= ThemeCapabilities.LightMode;
            }
            if (highContrast)
            {
                caps |= ThemeCapabilities.HighContrast;
            }
            return caps;
        }
        /// <summary>
        /// return the theme capabilities as configured by the user
        /// </summary>
        /// <returns></returns>
        private static ThemeCapabilities GetThemeCaps()
        {
            return GetThemeCaps(WindowsThemeDetector.GetDarkMode(), WindowsThemeDetector.GetHighContrast());
        }
        /// <summary>
        /// return the theme capabilities as configured by the user
        /// </summary>
        /// <returns></returns>
        public static ITheme GetTheme()
        {
            return Get(GetThemeCaps());
        }

        /// <summary>
        /// List of all Themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> THEMES = new();
        static ThemeRegistry()
        {
            //find all themes and add them to our theme list
            List<IThemeLookup> lookups = _themeLookups.OrderByDescending(l => l.Order).ToList();
            foreach (IThemeLookup l in lookups)
            {
                l.Lookup().ForEach(t => {
                    if (!THEMES.ContainsKey(t.Name))
                        THEMES.Add(t.Name, t);
                });
            }
        }
        /// <summary>
        /// returns the list of all theme names
        /// </summary>
        /// <returns></returns>
        public static List<string> ListNames()
        {
            return THEMES.Keys.ToList();
        }
        /// <summary>
        /// returns all themes
        /// </summary>
        /// <returns></returns>
        public static List<ITheme> List()
        {
            return THEMES.Values.ToList();
        }
        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITheme Get(string name)
        {
            return THEMES.ContainsKey(name) ? THEMES[name] : null;
        }
        /// <summary>
        /// return the theme with the matching capabilities
        /// </summary>
        /// <param name="caps"></param>
        /// <returns></returns>
        public static ITheme Get(ThemeCapabilities caps)
        {
            return List().FirstOrDefault(t => (t.Capabilities & caps) == caps);
        }
        #region Theme Plugins
        /// <summary>
        /// contains all plugins
        /// </summary>
        private static Dictionary<Type, IThemePlugin> _plugins = new Dictionary<Type, IThemePlugin>();
        /// <summary>
        /// returns all registered plugins
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyDictionary<Type, IThemePlugin> GetAllPlugins()
        {
            return new ReadOnlyDictionary<Type, IThemePlugin>(_plugins);
        }
        /// <summary>
        /// Add a plugin to handle additional controls for Themes that support it
        /// </summary>
        /// <typeparam name="T">the Control to handle</typeparam>
        /// <param name="plugin">the plugin handling the theming</param>
        public static void AddPlugin<T>(IThemePlugin plugin) where T : Control
        {
            _plugins.Add(typeof(T), plugin);
        }
        #endregion
    }
}
