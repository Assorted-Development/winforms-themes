using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// load external themes from JSON files
    /// </summary>
    public class FileThemeLookup : IThemeLookup
    {
        private readonly DirectoryInfo _folder;

        public FileThemeLookup(DirectoryInfo? folder = null)
        {
            if (folder == null)
            {
                folder = new DirectoryInfo("themes");
            }
            _folder = folder;
        }

        public int Order => Int32.MinValue;

        public List<ITheme> Lookup()
        {
            List<ITheme> results = new();
            if (_folder.Exists)
            {
                IEnumerable<FileInfo> files = _folder.EnumerateFiles("*.theme.json");
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