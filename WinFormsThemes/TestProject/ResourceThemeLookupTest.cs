using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ResourceThemeLookupTest
    {
        [TestMethod]
        public void ThemeInEmbeddedResourceShouldBeFound()
        {
            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .FromLookup(new ResourceThemeLookup())
                    .CompleteThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "Resource-Embedded");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-embedded-test", theme.Name);
        }

        [TestMethod]
        public void ThemeInResourceFileShouldBeFound()
        {
            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .FromLookup(new ResourceThemeLookup())
                    .CompleteThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-test", theme.Name);
        }

        [TestMethod]
        public void CheckDifferentFolderHandling()
        {
            var registry = IThemeRegistry.BUILDER
                .WithThemes()
                    .WithResourceLookup("CUSTOM_THEMING_PREFIX_")
                    .CompleteThemeList()
                .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-prefix-test", theme.Name);
        }
    }
}