﻿using MFBot_1701_E.Theming.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MFBot_1701_E.Theming
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
        public int Order => 10;

        public List<ITheme> Lookup()
        {
            List<ITheme> results = new List<ITheme>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyCompanyAttribute comp = a.GetCustomAttribute<AssemblyCompanyAttribute>();
                if(comp != null && comp.Company == "Microsoft Corporation")
                {
                    continue;
                }
                foreach (string res in a.GetManifestResourceNames())
                {
                    if (!res.Contains(RES_THEME_PREFIX)) continue;
                    ITheme theme = null;
                    using (Stream stream = a.GetManifestResourceStream(res))
                    using (StreamReader reader = new StreamReader(stream))
                        theme = FileTheme.Load(reader.ReadToEnd());
                    if (theme != null)
                    {
                        results.Add(theme);
                    }
                }
            }
            return results;
        }
    }
}
