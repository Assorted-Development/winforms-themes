using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ThemeRegistryTest
    {
        [TestMethod]
        public void GetShouldReturnCorrectTheme()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode);
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
        }

        [TestMethod]
        public void OnThemeChangedShouldFireOnChange()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            registry.Current = registry.Get();
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void OnThemeChangedShouldNotFireOnSameTheme()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            registry.Current = registry.Get();
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            registry.Current = registry.Get();
            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void TestGetThemeByName()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var theme = registry.Get("DARK_HIGH_CONTRAST");
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
            Assert.AreEqual(ThemeCapabilities.HighContrast, theme.Capabilities & ThemeCapabilities.HighContrast);
        }

        [TestMethod]
        public void TestListThemes()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var themes = registry.List();
            Assert.IsTrue(themes.Count > 0);
        }
    }
}