using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ResourceThemeLookupTest
    {
        [TestMethod]
        public void ThemeInEmbeddedResourceShouldBeFound()
        {
            var registry = IThemeRegistry.BUILDER.Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "Resource-Embedded");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-embedded-test", theme.Name);
        }

        [TestMethod]
        public void ThemeInResourceFileShouldBeFound()
        {
            var registry = IThemeRegistry.BUILDER.Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode, "Resource-File");
            Assert.IsNotNull(theme);
            Assert.AreEqual("resource-file-test", theme.Name);
        }
    }
}