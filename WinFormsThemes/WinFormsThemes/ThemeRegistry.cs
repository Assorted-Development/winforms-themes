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
        /// List of all Themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> THEMES = new();
        /// <summary>
        /// the current theme
        /// </summary>
        public static ITheme Current
        {
            get 
            {
                _current ??= Get();
                return _current;
            }
            set 
            { 
                _current = value;
                OnThemeChanged?.Invoke(value, EventArgs.Empty);
            }
        }
        #region Init
        /// <summary>
        /// true if InitThemes was already run successfully
        /// </summary>
        private static bool _themesAlreadyLoaded = false;
        /// <summary>
        /// Initialize the list of available themes
        /// </summary>
        private static void InitThemes()
        {
            if (_themesAlreadyLoaded) return;
            //find all themes and add them to our theme list
            List<IThemeLookup> lookups = _themeLookups.OrderByDescending(l => l.Order).ToList();
            foreach (IThemeLookup l in lookups)
            {
                l.Lookup().ForEach(t => {
                    if (!THEMES.ContainsKey(t.Name))
                        THEMES.Add(t.Name, t);
                });
            }
            _themesAlreadyLoaded = true;
        }
        #endregion
        /// <summary>
        /// add a plugin for looking up theme definitions
        /// </summary>
        /// <param name="plugin"></param>
        public static void AddThemeLookupPlugin(IThemeLookup plugin)
        {
            if (_themesAlreadyLoaded) throw new InvalidOperationException("Themes were already loaded - new theme lookups will not be run");
            _themeLookups.Insert(0, plugin);
        }
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
        public static ITheme Get()
        {
            return Get(GetThemeCaps());
        }
        /// <summary>
        /// returns the list of all theme names
        /// </summary>
        /// <returns></returns>
        public static List<string> ListNames()
        {
            InitThemes();
            return THEMES.Keys.ToList();
        }
        /// <summary>
        /// returns all themes
        /// </summary>
        /// <returns></returns>
        public static List<ITheme> List()
        {
            InitThemes();
            return THEMES.Values.ToList();
        }
        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITheme Get(string name)
        {
            InitThemes();
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
        public static ReadOnlyDictionary<Type, IThemePlugin> GetAllThemePlugins()
        {
            return new ReadOnlyDictionary<Type, IThemePlugin>(_plugins);
        }
        /// <summary>
        /// Add a plugin to handle additional controls for Themes that support it
        /// </summary>
        /// <typeparam name="T">the Control to handle</typeparam>
        /// <param name="plugin">the plugin handling the theming</param>
        public static void AddThemePlugin<T>(IThemePlugin plugin) where T : Control
        {
            _plugins.Add(typeof(T), plugin);
        }
        #endregion
    }
}
