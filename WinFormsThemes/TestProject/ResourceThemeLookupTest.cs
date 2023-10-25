using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ResourceThemeLookupTest : AbstractTestClass
    {
        [TestMethod]
        public void CheckDifferentFolderHandling()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithResourceLookup("CUSTOM_THEMING_PREFIX_")
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.GetTheme(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-prefix-test", theme.Name);
        }

        [TestMethod]
        public void ThemeInEmbeddedResourceShouldBeFound()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithLookup(new ResourceThemeLookup())
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.GetTheme(ThemeCapabilities.DarkMode, "Resource-Embedded");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-embedded-test", theme.Name);
        }

        [TestMethod]
        public void ThemeInResourceFileShouldBeFound()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                .WithThemes()
                    .WithLookup(new ResourceThemeLookup())
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.GetTheme(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-test", theme.Name);
        }

        [TestMethod]
        public void CheckDifferentPrefixHandling()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                .WithThemes()
                    .WithResourceLookup("CUSTOM_THEMING_PREFIX_")
                    .FinishThemeList()
                .Build();
            ITheme? theme = registry.GetTheme(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-prefix-test", theme.Name);
        }
    }
}