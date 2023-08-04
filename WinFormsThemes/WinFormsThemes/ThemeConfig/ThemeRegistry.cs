using WinFormsThemes.Utilities;

namespace WinFormsThemes
{
    /// <summary>
    /// registry for all Themes
    /// </summary>
    public class ThemeRegistry : IThemeRegistry
    {
        /// <summary>
        /// dictionary of all themes
        /// </summary>
        private readonly Dictionary<string, ITheme> _themes;

        /// <summary>
        /// the current theme
        /// </summary>
        private ITheme? _current = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="themes">all available themes with their name as key and the <see cref="ITheme"/> instance as value</param>
        public ThemeRegistry(Dictionary<string, ITheme> themes)
        {
            _themes = themes;
        }

        /// <summary>
        /// Event that gets triggered when the <see cref="Current"/> theme has changed
        /// </summary>
        public event EventHandler? OnThemeChanged;

        public ITheme? Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (_current != value)
                {
                    _current = value;
                    OnThemeChanged?.Invoke(value, EventArgs.Empty);
                }
            }
        }

        public ITheme? Get()
        {
            return Get(GetThemeCaps());
        }

        public ITheme? Get(string name)
        {
            return _themes.ContainsKey(name) ? _themes[name] : null;
        }

        public ITheme? Get(ThemeCapabilities caps, params string[] advancedCapabilitiesFilters)
        {
            return _themes.Values.Where(t => (t.Capabilities & caps) == caps)
                .Where(t => advancedCapabilitiesFilters.All(f => t.AdvancedCapabilities.Contains(f)))
                .FirstOrDefault();
        }

        public List<ITheme> List()
        {
            return _themes.Values.ToList();
        }

        public List<string> ListNames()
        {
            return _themes.Keys.ToList();
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
    }
}