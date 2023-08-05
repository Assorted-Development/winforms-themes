using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.ObjectModel;
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
        /// the logger to use
        /// </summary>
        private ILogger<IThemeRegistryBuilder> _logger;

        /// <summary>
        /// the logger factory to use
        /// </summary>
        private ILoggerFactory _loggerFactory;

        /// <summary>
        /// the builder for adding themes
        /// </summary>
        private ThemeRegistryThemeListBuilder? _themeListBuilder;

        public ThemeRegistryBuilder(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = new Logger<IThemeRegistryBuilder>(_loggerFactory);
        }

        public IThemeRegistryBuilder AddThemePlugin<T>(IThemePlugin plugin) where T : Control
        {
            Type t = typeof(T);
            if (_themePlugins.ContainsKey(t))
            {
                _logger.LogError($"ThemePlugin for {t.Name} already added");
                throw new InvalidOperationException($"ThemePlugin for {t.Name} already added");
            }
            _logger.LogTrace($"Adding ThemePlugin for {t.Name}");
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
                _logger.LogDebug("No themes were set explicitly, initializing with default themes");
                _themeListBuilder = new ThemeRegistryThemeListBuilder(this, _loggerFactory);
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
            var registry = new ThemeRegistry(themes);
            return registry;
        }

        public IThemeRegistryThemeListBuilder WithThemes()
        {
            if (_themeListBuilder != null)
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
        private readonly ILogger<IThemeRegistryThemeListBuilder> _logger = new Logger<IThemeRegistryThemeListBuilder>(new NullLoggerFactory());

        /// <summary>
        /// the logger factory to use
        /// </summary>
        private readonly ILoggerFactory _loggerFactory = new NullLoggerFactory();

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
                _logger.LogError($"Theme with name {theme.Name} already exists");
                throw new InvalidOperationException($"Theme with name {theme.Name} already exists");
            }
            _logger.LogTrace($"Adding theme {theme.Name}");
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
            _logger.LogTrace($"Adding file theme lookup for folder: {themeFolder?.FullName}");
            _lookups.Add(new FileThemeLookup(themeFolder));
            return this;
        }

        public IThemeRegistryThemeListBuilder WithLookup(IThemeLookup themeLookup)
        {
            _logger.LogTrace($"Adding theme lookup {themeLookup.GetType().Name}");
            _lookups.Add(themeLookup);
            return this;
        }

        public IThemeRegistryThemeListBuilder WithResourceLookup(string? resourcePrefix = null)
        {
            _logger.LogTrace($"Adding resource theme lookup for prefix: {resourcePrefix}");
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
            _logger.LogTrace($"Building theme list from {_lookups.Count} lookups");
            //loop through all lookups
            foreach (IThemeLookup lookup in _lookups)
            {
                try
                {
                    lookup.UseLogger(_loggerFactory);
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
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error looking up themes from {lookup.GetType().Name}");
                }
            }
            return _themes;
        }
    }
}