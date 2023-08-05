using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections;
using System.Reflection;
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
        /// the logger to use
        /// </summary>
        private ILogger<IThemeLookup> _logger = new Logger<IThemeLookup>(new NullLoggerFactory());

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="prefix">the prefix to detect the themes in the resources</param>
        public ResourceThemeLookup(string? prefix = null)
        {
            prefix ??= RES_THEME_PREFIX;
            _resThemePrefix = prefix;
        }

        public int Order => Int32.MinValue;

        public List<ITheme> Lookup()
        {
            List<ITheme> results = new();
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
                        ITheme? theme;
                        using (Stream? stream = a.GetManifestResourceStream(res))
                            theme = HandleEmbeddedResource(stream);
                        if (theme != null)
                        {
                            results.Add(theme);
                        }
                        else
                        {
                            _logger.LogDebug("Skipping invalid theme {res} in assembly {name}", res, name);
                        }
                    }
                    else if (res.EndsWith(".resources"))
                    {
                        using Stream? stream = a.GetManifestResourceStream(res);
                        if (stream != null)
                            results.AddRange(HandleResource(stream));
                    }
                    else
                    {
                        _logger.LogDebug("Skipping unknown resource {res} in assembly {name}", res, name);
                    }
                }
            }
            return results;
        }

        public void UseLogger(ILoggerFactory loggerFactory)
        {
            _logger = new Logger<IThemeLookup>(loggerFactory);
        }

        /// <summary>
        /// Handle Resources embedded directly into the dll
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static ITheme? HandleEmbeddedResource(Stream? stream)
        {
            if (stream == null) return null;
            using StreamReader reader = new(stream);
            return FileTheme.Load(reader.ReadToEnd());
        }

        /// <summary>
        /// handle Resources added to a resource file
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private List<ITheme> HandleResource(Stream stream)
        {
            using var resourceReader = new System.Resources.ResourceReader(stream);
            List<ITheme> results = new();
            foreach (DictionaryEntry entry in resourceReader)
            {
                if (entry.Key is string key && key.StartsWith(_resThemePrefix))
                {
                    if (entry.Value is string value)
                    {
                        ITheme? theme = FileTheme.Load(value);
                        if (theme != null)
                        {
                            results.Add(theme);
                        }
                        else
                        {
                            _logger.LogDebug("Skipping invalid theme {key} in resource", key);
                        }
                    }
                }
            }
            return results;
        }
    }
}