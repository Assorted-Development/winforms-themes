using System.Collections;
using System.Reflection;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// load Themes from internal resource files
    /// </summary>
    internal class ResourceThemeLookup : IThemeLookup
    {
        /// <summary>
        /// the prefix to detect the themes in the resources
        /// </summary>
        private const string RES_THEME_PREFIX = "CONFIG_THEMING_THEME_";

        public int Order => Int32.MinValue;

        public List<ITheme> Lookup()
        {
            List<ITheme> results = new List<ITheme>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                string name = a.FullName ?? "";
                if (a.IsDynamic)
                {
                    //Dynamic libraries (e.g. Expression.Compile) do not support reading resources
                    //and would throw an exception
                    continue;
                }
                AssemblyCompanyAttribute comp = a.GetCustomAttribute<AssemblyCompanyAttribute>();
                if (comp != null && comp.Company == "Microsoft Corporation")
                {
                    continue;
                }
                foreach (string res in a.GetManifestResourceNames())
                {
                    if (res.Contains(RES_THEME_PREFIX))
                    {
                        ITheme? theme;
                        using (Stream? stream = a.GetManifestResourceStream(res))
                            theme = HandleEmbeddedResource(stream);
                        if (theme != null)
                        {
                            results.Add(theme);
                        }
                    }
                    else if (res.EndsWith(".resources"))
                    {
                        using (Stream stream = a.GetManifestResourceStream(res))
                            results.AddRange(HandleResource(stream));
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Handle Resources embedded directly into the dll
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static ITheme? HandleEmbeddedResource(Stream? stream)
        {
            if (stream == null) return null;
            using (StreamReader reader = new StreamReader(stream))
                return FileTheme.Load(reader.ReadToEnd());
        }

        /// <summary>
        /// handle Resources added to a resource file
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static List<ITheme> HandleResource(Stream stream)
        {
            using (var resourceReader = new System.Resources.ResourceReader(stream))
            {
                List<ITheme> results = new List<ITheme>();
                foreach (DictionaryEntry entry in resourceReader)
                {
                    if (entry.Key is string key && key.StartsWith(RES_THEME_PREFIX))
                    {
                        if (entry.Value is string value)
                        {
                            results.Add(FileTheme.Load(value));
                        }
                    }
                }
                return results;
            }//dispose
        }
    }
}