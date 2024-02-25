using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    /// <summary>
    /// load external themes from JSON files
    /// </summary>
    public class FileThemeLookup : IThemeLookup
    {
        private readonly DirectoryInfo _folder;

        /// <summary>
        /// the logger to use
        /// </summary>
        private ILogger<IThemeLookup> _logger = new Logger<IThemeLookup>(new NullLoggerFactory());

        public FileThemeLookup(DirectoryInfo? folder = null)
        {
            folder ??= new DirectoryInfo("themes");
            _folder = folder;
        }

        public int Order => int.MinValue;

        public IList<ITheme> Lookup()
        {
            List<ITheme> results = new();
            if (_folder.Exists)
            {
                IEnumerable<FileInfo> files = _folder.EnumerateFiles("*.theme.json");
                _logger.LogDebug("found {count} theme files in {folder}", files.Count(), _folder.FullName);
                foreach (FileInfo file in files)
                {
                    ITheme? theme = FileTheme.Load(File.ReadAllText(file.FullName), _logger);
                    if (theme is not null)
                    {
                        results.Add(theme);
                    }
                }
            }
            else
            {
                _logger.LogDebug("theme folder {folder} does not exist", _folder.FullName);
            }
            return results;
        }

        public void UseLogger(ILoggerFactory loggerFactory)
        {
            _logger = new Logger<IThemeLookup>(loggerFactory);
        }
    }
}
