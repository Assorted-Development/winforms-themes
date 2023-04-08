using MFBot_1701_E.Theming.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// load external themes from JSON files
    /// </summary>
    internal class FileThemeLookup : IThemeLookup
    {
        public int Order => 20;

        public List<ITheme> Lookup()
        {
            var results = new List<ITheme>();
            var dir = new DirectoryInfo("themes");
            if(dir.Exists)
            {
                var files = dir.EnumerateFiles("*.theme.json");
                foreach( var file in files)
                {
                    ITheme theme = FileTheme.Load(File.ReadAllText(file.FullName));
                    if(theme != null)
                    {
                        results.Add(theme);
                    }
                }
            }
            return results;
        }
    }
}
