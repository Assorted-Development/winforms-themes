using MFBot_1701_E.Theming.Themes;
using System.Collections.Generic;
using System.IO;

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
            List<ITheme> results = new List<ITheme>();
            DirectoryInfo dir = new DirectoryInfo("themes");
            if (dir.Exists)
            {
                IEnumerable<FileInfo> files = dir.EnumerateFiles("*.theme.json");
                foreach (FileInfo file in files)
                {
                    ITheme theme = FileTheme.Load(File.ReadAllText(file.FullName));
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
