using TestProject.Properties;
using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class FileThemeLookupTest : AbstractTestClass
    {
        [TestMethod]
        public void CheckDifferentFolderHandling()
        {
            if (Directory.Exists("themes"))
            {
                Directory.Delete("themes", true);
            }
            DirectoryInfo dir = Directory.CreateDirectory("themes2");
            File.WriteAllText("themes2\\test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_theme);

            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithFileLookup(dir)
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.Get(ThemeCapabilities.DarkMode, "File", "OK");
            Assert.IsNotNull(theme);
            Assert.AreEqual("file-ok-test", theme.Name);
        }

        [TestMethod]
        public void ShouldFindFilesThemeFiles()
        {
            Directory.CreateDirectory("themes");
            File.WriteAllText("themes\\test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_theme);
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.Get(ThemeCapabilities.DarkMode, "File", "OK");
            Assert.IsNotNull(theme);
            Assert.AreEqual("file-ok-test", theme.Name);
        }

        [TestMethod]
        public void ShouldNotFindFilesOutsideDirectory()
        {
            File.WriteAllText("test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_2_theme);
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.Get(ThemeCapabilities.DarkMode, "File", "Error");
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void ShouldNotFindFilesWithDifferentExtension()
        {
            Directory.CreateDirectory("themes");
            File.WriteAllText("themes\\test.theme.json.disabled", Resources.CONFIG_THEMING_THEME_FILE_TEST_2_theme);

            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.Get(ThemeCapabilities.DarkMode, "File", "Error");
            Assert.IsNull(theme);
        }
    }
}