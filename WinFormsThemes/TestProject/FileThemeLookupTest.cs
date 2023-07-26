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
                    .CompleteThemeList()
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
                    .CompleteThemeList()
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
                    .CompleteThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "File", "Error");
            Assert.IsNull(theme);
        }
    }
}