using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ThemeRegistryTest : AbstractTestClass
    {
        [TestMethod]
        public void GetShouldReturnCorrectTheme()
        {
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            var theme = registry.Get(ThemeCapabilities.DarkMode);
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
        }

        [TestMethod]
        public void OnThemeChangedShouldFireOnChange()
        {
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithCurrentThemeSelector((r) => r.Get())
                            .Build();
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            var current = registry.Current;
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void OnThemeChangedShouldNotFireOnSameTheme()
        {
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithCurrentThemeSelector((r) => r.Get())
                            .Build();
            var current = registry.Current;
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            current = registry.Current;
            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void CurrentShouldThrowWithoutSelector()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .Build();
            Assert.ThrowsException<InvalidOperationException>(() => registry.Current);
        }

        [TestMethod]
        public void TestGetThemeByName()
        {
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            var theme = registry.Get("DARK_HIGH_CONTRAST");
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
            Assert.AreEqual(ThemeCapabilities.HighContrast, theme.Capabilities & ThemeCapabilities.HighContrast);
        }

        [TestMethod]
        public void TestListThemes()
        {
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            var themes = registry.List();
            Assert.IsTrue(themes.Count > 0);
        }
    }
}