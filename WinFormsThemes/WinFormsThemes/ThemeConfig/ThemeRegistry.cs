using WinFormsThemes.ThemeConfig;
using WinFormsThemes.Utilities;

namespace WinFormsThemes
{
    /// <summary>
    /// registry for all Themes
    /// </summary>
    internal class ThemeRegistry : IThemeRegistry
    {
        /// <summary>
        /// dictionary of all themes
        /// </summary>
        private readonly Dictionary<string, ITheme> _themes;

        /// <summary>
        /// the current theme
        /// </summary>
        private ITheme? _current;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="themes">all available themes with their name as key and the <see cref="ITheme"/> instance as value</param>
        public ThemeRegistry(Dictionary<string, ITheme> themes, CurrentThemeSelector? selector)
        {
            _themes = themes;
            CurrentThemeSelector = selector;
        }

        /// <summary>
        /// Event that gets triggered when the <see cref="CurrentTheme"/> theme has changed
        /// </summary>
        public event EventHandler? OnThemeChanged;

        public ITheme? CurrentTheme
        {
            get
            {
                if (CurrentThemeSelector is null)
                {
                    throw new InvalidOperationException("CurrentThemeSelector is null");
                }
                ITheme? newTheme = CurrentThemeSelector(this);
                if (newTheme != _current)
                {
                    _current = newTheme;
                    OnThemeChanged?.Invoke(this, EventArgs.Empty);
                }
                return newTheme;
            }
        }

        /// <summary>
        /// A simple way to provide a current theme through <see cref="IThemeRegistry.CurrentTheme"/>.
        /// </summary>
        private CurrentThemeSelector? CurrentThemeSelector { get; }

        public ITheme? GetTheme()
        {
            return GetTheme(getThemeCaps());
        }

        public ITheme? GetTheme(string name)
        {
            return _themes.ContainsKey(name) ? _themes[name] : null;
        }

        public ITheme? GetTheme(ThemeCapabilities caps, params string[] advancedCapabilitiesFilters)
        {
            return _themes.Values
                .Where(t => (t.Capabilities & caps) == caps)
                .FirstOrDefault(t => advancedCapabilitiesFilters.All(f => t.AdvancedCapabilities.Contains(f)));
        }

        public IList<ITheme> ListThemes()
        {
            return _themes.Values.ToList();
        }

        public IList<string> ListNames()
        {
            return _themes.Keys.ToList();
        }

        /// <summary>
        /// return the theme capabilities
        /// </summary>
        private static ThemeCapabilities getThemeCaps(bool dark, bool highContrast)
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
        private static ThemeCapabilities getThemeCaps()
        {
            return getThemeCaps(WindowsThemeDetector.GetDarkMode(), WindowsThemeDetector.GetHighContrast());
        }
    }
}
