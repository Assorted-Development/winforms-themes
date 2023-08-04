using TestProject.Properties;
using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class FileThemeLookupTest
    {
        [TestMethod]
        public void ShouldFindFilesThemeFiles()
        {
            Directory.CreateDirectory("themes");
            File.WriteAllText("themes\\test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_theme);
            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .FromLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "File", "OK");
            Assert.IsNotNull(theme);
            Assert.AreEqual("file-ok-test", theme.Name);
        }

        [TestMethod]
        public void ShouldNotFindFilesOutsideDirectory()
        {
            File.WriteAllText("test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_2_theme);
            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .FromLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "File", "Error");
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void ShouldNotFindFilesWithDifferentExtension()
        {
            Directory.CreateDirectory("themes");
            File.WriteAllText("themes\\test.theme.json.disabled", Resources.CONFIG_THEMING_THEME_FILE_TEST_2_theme);

            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .FromLookup(new FileThemeLookup())
                    .FinishThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "File", "Error");
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void CheckDifferentFolderHandling()
        {
            if (Directory.Exists("themes"))
            {
                Directory.Delete("themes", true);
            }
            var dir = Directory.CreateDirectory("themes2");
            File.WriteAllText("themes2\\test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_theme);

            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .WithFileLookup(dir)
                    .FinishThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "File", "OK");
            Assert.IsNotNull(theme);
            Assert.AreEqual("file-ok-test", theme.Name);
        }
    }
}