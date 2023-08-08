using System.Collections.ObjectModel;
using WinFormsThemes.ThemeConfig;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// implementation of the builder
    /// </summary>
    internal class ThemeRegistryBuilder : IThemeRegistryBuilder
    {
        /// <summary>
        /// dictionary of all theme plugins
        /// </summary>
        private readonly Dictionary<Type, IThemePlugin> _themePlugins = new();

        /// <summary>
        /// the given selector for the current theme
        /// </summary>
        private SimpleCurrentThemeSelector? _currentThemeSelector;

        /// <summary>
        /// the builder for adding themes
        /// </summary>
        private ThemeRegistryThemeListBuilder? _themeListBuilder;

        public IThemeRegistryBuilder AddThemePlugin<T>(IThemePlugin plugin) where T : Control
        {
            Type t = typeof(T);
            if (_themePlugins.ContainsKey(t))
            {
                throw new InvalidOperationException($"ThemePlugin for {t.Name} already added");
            }
            _themePlugins.Add(t, plugin);
            return this;
        }

        //create a building pattern for ThemeRegistry
        public IThemeRegistry Build()
        {
            //build the list of themes
            Dictionary<string, ITheme> themes;
            if (_themeListBuilder == null)
            {
                //themes were not set explicitly so initialize with default themes
                _themeListBuilder = new ThemeRegistryThemeListBuilder(this);
                _themeListBuilder.FromDefaultLookups()
                                 .AddDefaultThemes();
            }
            themes = _themeListBuilder.Build();
            //initialize the themes if plugins were set
            if (_themePlugins.Count > 0)
            {
                var plugins = new ReadOnlyDictionary<Type, IThemePlugin>(_themePlugins);
                themes.Values.ToList().ForEach(theme => theme.ThemePlugins = plugins);
            }
            var registry = new ThemeRegistry(themes, _currentThemeSelector);
            return registry;
        }

        public IThemeRegistryBuilder WithCurrentThemeSelector(SimpleCurrentThemeSelector selector)
        {
            if (_currentThemeSelector != null)
            {
                throw new InvalidOperationException("WithCurrentThemeSelector() can only be called once");
            }
            _currentThemeSelector = selector;
            return this;
        }

        public IThemeRegistryThemeListBuilder WithThemes()
        {
            if (_themeListBuilder != null)
            {
                throw new InvalidOperationException("WithThemes() can only be called once");
            }
            _themeListBuilder = new ThemeRegistryThemeListBuilder(this);
            return _themeListBuilder;
        }
    }

    /// <summary>
    /// allows specifying the list of themes
    /// </summary>
    internal class ThemeRegistryThemeListBuilder : IThemeRegistryThemeListBuilder
    {
        /// <summary>
        /// the list of lookups to use
        /// </summary>
        private readonly List<IThemeLookup> _lookups = new();

        /// <summary>
        /// the parent builder object
        /// </summary>
        private readonly ThemeRegistryBuilder _parent;

        /// <summary>
        /// the list of found themes
        /// </summary>
        private readonly Dictionary<string, ITheme> _themes = new();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="parent"></param>
        public ThemeRegistryThemeListBuilder(ThemeRegistryBuilder parent)
        {
            _parent = parent;
        }

        public IThemeRegistryThemeListBuilder AddDefaultThemes()
        {
            AddTheme(new DefaultLightTheme());
            AddTheme(new DefaultDarkTheme());
            AddTheme(new HighContrastDarkTheme());
            return this;
        }

        public IThemeRegistryThemeListBuilder AddTheme(ITheme theme)
        {
            if (_themes.ContainsKey(theme.Name))
            {
                throw new InvalidOperationException($"Theme with name {theme.Name} already exists");
            }
            _themes.Add(theme.Name, theme);
            return this;
        }

        public IThemeRegistryBuilder FinishThemeList()
        {
            return _parent;
        }

        public IThemeRegistryThemeListBuilder FromDefaultLookups()
        {
            _lookups.Add(new FileThemeLookup());
            _lookups.Add(new ResourceThemeLookup());
            return this;
        }

        public IThemeRegistryThemeListBuilder WithFileLookup(DirectoryInfo? themeFolder = null)
        {
            _lookups.Add(new FileThemeLookup(themeFolder));
            return this;
        }

        public IThemeRegistryThemeListBuilder WithLookup(IThemeLookup themeLookup)
        {
            _lookups.Add(themeLookup);
            return this;
        }

        public IThemeRegistryThemeListBuilder WithResourceLookup(string? resourcePrefix = null)
        {
            _lookups.Add(new ResourceThemeLookup(resourcePrefix));
            return this;
        }

        /// <summary>
        /// Build the final list of themes
        /// </summary>
        /// <returns></returns>
        internal Dictionary<string, ITheme> Build()
        {
            //sort the lookups in descending order
            _lookups.Sort((x, y) => y.Order.CompareTo(x.Order));
            //loop through all lookups
            foreach (IThemeLookup lookup in _lookups)
            {
                //get the themes from the lookup
                List<ITheme> themes = lookup.Lookup();
                //loop through all themes
                foreach (ITheme theme in themes)
                {
                    //add the theme to the list but check if it already exists first
                    if (!_themes.ContainsKey(theme.Name))
                    {
                        AddTheme(theme);
                    }
                }
            }
            return _themes;
        }
    }
}