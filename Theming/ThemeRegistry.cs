using System;
using System.Collections.Generic;
using System.Linq;
using de.mfbot.MFBot_NG.Basisbibliothek;

namespace MFBot_1701_E.Theming
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
        private static ITheme _current = null;
        /// <summary>
        /// all objects to detect existing themes
        /// </summary>
        private static readonly List<IThemeLookup> themeLookups = new()
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
            get
            {
                if(_current == null)
                {
                    InitializeTheme();
                }
                return _current;
            }
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
            if (GlobalSettings.Settings.GLOBALDSKIN_AUTO)
            {
                return GetThemeCaps(WindowsThemeDetector.GetDarkMode(), WindowsThemeDetector.GetHighContrast());
            }
            return GetThemeCaps(GlobalSettings.Settings.GLOBALDSKIN, GlobalSettings.Settings.GLOBALDSKINContrast);
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
        /// Initialize the current theme and register for changes
        /// </summary>
        private static void InitializeTheme()
        {
            _current = GetTheme();
            GlobalSettings.Settings.OnChanged += Settings_OnChanged;
        }
        /// <summary>
        /// update current theme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Settings_OnChanged(object sender, SettingsChangedEventArgs e)
        {
            if(e.Key is "GLOBDSKIN" or "GLOBDSKINCONT" or "GLOBDSKIN.AUTO")
            {
                ITheme newTheme = GetTheme();
                bool changed = newTheme != _current;
                _current = newTheme;
                if(changed && OnThemeChanged != null)
                {
                    OnThemeChanged.Invoke(sender, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// List of all Themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> THEMES = new();
        static ThemeRegistry()
        {
            //find all themes and add them to our theme list
            List<IThemeLookup> lookups = themeLookups.OrderByDescending(l => l.Order).ToList();
            foreach(IThemeLookup l in lookups)
            {
                l.Lookup().ForEach(t => {
                    if(!THEMES.ContainsKey(t.Name))
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
    }
}
