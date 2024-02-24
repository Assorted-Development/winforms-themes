using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
        private CurrentThemeSelector? _currentThemeSelector;

        /// <summary>
        /// the logger to use
        /// </summary>
        private ILogger<IThemeRegistryBuilder> _logger;

        /// <summary>
        /// the logger factory to use
        /// </summary>
        private ILoggerFactory _loggerFactory = new NullLoggerFactory();

        /// <summary>
        /// the builder for adding themes
        /// </summary>
        private ThemeRegistryThemeListBuilder? _themeListBuilder;

        public ThemeRegistryBuilder()
        {
            _logger = new Logger<IThemeRegistryBuilder>(_loggerFactory);
        }

        public IThemeRegistryBuilder AddThemePlugin(IThemePlugin plugin)
        {
            Type t = plugin.GetSupportedType();
            if (_themePlugins.ContainsKey(t))
            {
                _logger.LogError("ThemePlugin for {ThemeName} already added", t.Name);
                throw new InvalidOperationException($"ThemePlugin for {t.Name} already added");
            }

            _logger.LogTrace("Adding ThemePlugin for {ThemeName}", t.Name);
            _themePlugins.Add(t, plugin);

            return this;
        }

        /// <summary>
        /// create a building pattern for ThemeRegistry
        /// </summary>
        public IThemeRegistry Build()
        {
            //build the list of themes
            if (_themeListBuilder is null)
            {
                //themes were not set explicitly so initialize with default themes
                _logger.LogDebug("No themes were set explicitly, initializing with default themes");
                _themeListBuilder = new ThemeRegistryThemeListBuilder(this, _loggerFactory);
                _themeListBuilder.FromDefaultLookups()
                                 .AddDefaultThemes();
            }

            Dictionary<string, ITheme> themes = _themeListBuilder.Build();
            //initialize the themes if plugins were set
            if (_themePlugins.Count > 0)
            {
                ReadOnlyDictionary<Type, IThemePlugin> plugins = new(_themePlugins);
                themes.Values.ToList().ForEach(theme => theme.ThemePlugins = plugins);
            }
            return new ThemeRegistry(themes, _currentThemeSelector);
        }

        public IThemeRegistryBuilder SetLoggerFactory(ILoggerFactory factory)
        {
            ArgumentNullException.ThrowIfNull(factory);

            if (_loggerFactory.GetType() != typeof(NullLoggerFactory))
            {
                throw new InvalidOperationException("EnableLogging() can only be called once");
            }
            _loggerFactory = factory;
            _logger = new Logger<IThemeRegistryBuilder>(_loggerFactory);
            return this;
        }

        public IThemeRegistryBuilder WithCurrentThemeSelector(CurrentThemeSelector selector)
        {
            if (_currentThemeSelector is not null)
            {
                throw new InvalidOperationException("WithCurrentThemeSelector() can only be called once");
            }
            _currentThemeSelector = selector;
            return this;
        }

        public IThemeRegistryThemeListBuilder WithThemes()
        {
            if (_themeListBuilder is not null)
            {
                _logger.LogError("WithThemes() can only be called once");
                throw new InvalidOperationException("WithThemes() can only be called once");
            }
            _themeListBuilder = new ThemeRegistryThemeListBuilder(this, _loggerFactory);
            return _themeListBuilder;
        }
    }

    /// <summary>
    /// allows specifying the list of themes
    /// </summary>
    internal class ThemeRegistryThemeListBuilder : IThemeRegistryThemeListBuilder
    {
        /// <summary>
        /// the logger to use
        /// </summary>
        private readonly ILogger<IThemeRegistryThemeListBuilder> _logger;

        /// <summary>
        /// the logger factory to use
        /// </summary>
        private readonly ILoggerFactory _loggerFactory;

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
        /// <param name="parent">the owning ThemeRegistryBuilder object</param>
        /// <param name="loggerFactory">the loggerFactory to use</param>
        public ThemeRegistryThemeListBuilder(ThemeRegistryBuilder parent, ILoggerFactory loggerFactory)
        {
            _parent = parent;
            _loggerFactory = loggerFactory;
            _logger = new Logger<IThemeRegistryThemeListBuilder>(_loggerFactory);
        }

        public IThemeRegistryThemeListBuilder AddDefaultThemes()
        {
            _logger.LogTrace("Adding default themes");
            AddTheme(new DefaultLightTheme());
            AddTheme(new DefaultDarkTheme());
            AddTheme(new HighContrastDarkTheme());
            return this;
        }

        public IThemeRegistryThemeListBuilder AddTheme(ITheme theme)
        {
            if (_themes.ContainsKey(theme.Name))
            {
                _logger.LogError("Theme with name {ThemeName} already exists", theme.Name);
                throw new InvalidOperationException($"Theme with name {theme.Name} already exists");
            }
            _logger.LogTrace("Adding theme {ThemeName}", theme.Name);
            theme.UseLogger(_loggerFactory);
            _themes.Add(theme.Name, theme);
            return this;
        }

        public IThemeRegistryBuilder FinishThemeList()
        {
            return _parent;
        }

        public IThemeRegistryThemeListBuilder FromDefaultLookups()
        {
            _logger.LogTrace("Adding default lookups");
            _lookups.Add(new FileThemeLookup());
            _lookups.Add(new ResourceThemeLookup());
            return this;
        }

        public IThemeRegistryThemeListBuilder WithFileLookup(DirectoryInfo? themeFolder = null)
        {
            _logger.LogTrace("Adding file theme lookup for folder: {ThemeFolder}", themeFolder?.FullName);
            _lookups.Add(new FileThemeLookup(themeFolder));
            return this;
        }

        public IThemeRegistryThemeListBuilder WithLookup(IThemeLookup themeLookup)
        {
            _logger.LogTrace("Adding theme lookup {ThemeLookupName}", themeLookup.GetType().Name);
            _lookups.Add(themeLookup);
            return this;
        }

        public IThemeRegistryThemeListBuilder WithResourceLookup(string? resourcePrefix = null)
        {
            _logger.LogTrace("Adding resource theme lookup for prefix: {ResourcePrefix}", resourcePrefix);
            _lookups.Add(new ResourceThemeLookup(resourcePrefix));
            return this;
        }

        /// <summary>
        /// Build the final list of themes
        /// </summary>
        internal Dictionary<string, ITheme> Build()
        {
            //sort the lookups in descending order
            _lookups.Sort((x, y) => y.Order.CompareTo(x.Order));
            _logger.LogTrace("Building theme list from {LookupCount} lookups", _lookups.Count);
            //loop through all lookups
            foreach (IThemeLookup lookup in _lookups)
            {
                try
                {
                    lookup.UseLogger(_loggerFactory);

                    //get the themes from the lookup and loop through them
                    foreach (ITheme theme in lookup.Lookup())
                    {
                        //add the theme to the list but check if it already exists first
                        if (!_themes.ContainsKey(theme.Name))
                        {
                            AddTheme(theme);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error looking up themes from {ThemeLookupName}", lookup.GetType().Name);
                }
            }
            return _themes;
        }
    }
}
