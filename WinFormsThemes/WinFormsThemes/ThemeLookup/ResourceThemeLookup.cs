using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// load Themes from internal resource files
    /// </summary>
    public class ResourceThemeLookup : IThemeLookup
    {
        /// <summary>
        /// the prefix to detect the themes in the resources
        /// </summary>
        private const string RES_THEME_PREFIX = "CONFIG_THEMING_THEME_";

        private readonly string _resThemePrefix;

        /// <summary>
        /// the result list of themes
        /// </summary>
        private readonly List<ITheme> _themes = new();

        /// <summary>
        /// the logger to use
        /// </summary>
        private ILogger<IThemeLookup> _logger = new Logger<IThemeLookup>(new NullLoggerFactory());

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="prefix">the prefix to detect the themes in the resources</param>
        public ResourceThemeLookup(string? prefix = null)
        {
            _resThemePrefix = prefix ?? RES_THEME_PREFIX;
        }

        public int Order => Int32.MinValue;

        public List<ITheme> Lookup()
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = a.FullName ?? "";
                if (a.IsDynamic)
                {
                    //Dynamic libraries (e.g. Expression.Compile) do not support reading resources
                    //and would throw an exception
                    _logger.LogTrace("Skipping dynamic assembly {name}", name);
                    continue;
                }
                AssemblyCompanyAttribute? comp = a.GetCustomAttribute<AssemblyCompanyAttribute>();
                if (comp != null && comp.Company == "Microsoft Corporation")
                {
                    _logger.LogTrace("Skipping Microsoft assembly {name}", name);
                    continue;
                }
                foreach (string res in a.GetManifestResourceNames())
                {
                    if (res.Contains(_resThemePrefix))
                    {
                        using (Stream? stream = a.GetManifestResourceStream(res))
                            HandleEmbeddedResource(stream, res);
                    }
                    else if (res.EndsWith(".resources"))
                    {
                        HandleResource(res, a);
                    }
                }
            }
            return _themes;
        }

        public void UseLogger(ILoggerFactory loggerFactory)
        {
            _logger = new Logger<IThemeLookup>(loggerFactory);
        }

        /// <summary>
        /// Add a theme to the resultlist
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="resName"></param>
        private void Add(ITheme? theme, string resName)
        {
            if (theme != null)
                _themes.Add(theme);
            else
                _logger.LogDebug("Skipping invalid theme {key} in resource", resName);
        }

        /// <summary>
        /// Handle Resources embedded directly into the dll
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="resName"></param>
        private void HandleEmbeddedResource(Stream? stream, string resName)
        {
            if (stream != null)
            {
                using StreamReader reader = new(stream);
                Add(FileTheme.Load(reader.ReadToEnd()), resName);
            }
        }

        /// <summary>
        /// handle Resources added to a resource file
        /// </summary>
        /// <param name="stream"></param>
        private void HandleResource(string resourceName, Assembly assembly)
        {
            var resBaseName = resourceName.Substring(0, resourceName.IndexOf(".resources"));
            var rm = new ResourceManager(resBaseName, assembly);
            ResourceSet? resourceSet = rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            if (resourceSet == null) return;
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key is string key && key.StartsWith(_resThemePrefix) &&
                    entry.Value is string value)
                {
                    Add(FileTheme.Load(value), key);
                }
            }
        }
    }
}