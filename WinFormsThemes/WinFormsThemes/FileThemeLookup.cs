﻿using WinFormsThemes.Themes;

namespace WinFormsThemes
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
                    ITheme? theme = FileTheme.Load(File.ReadAllText(file.FullName));
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