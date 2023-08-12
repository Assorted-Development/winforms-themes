using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ThemeRegistryTest : AbstractTestClass
    {
        [TestMethod]
        public void GetShouldReturnCorrectTheme()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            ITheme? theme = registry.Get(ThemeCapabilities.DarkMode);
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
        }

        [TestMethod]
        public void OnThemeChangedShouldFireOnChange()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithCurrentThemeSelector((r) => r.Get())
                            .Build();
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            ITheme? current = registry.Current;
            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void OnThemeChangedShouldNotFireOnSameTheme()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithCurrentThemeSelector((r) => r.Get())
                            .Build();
            ITheme? current = registry.Current;
            bool fired = false;
            registry.OnThemeChanged += (sender, args) => fired = true;
            current = registry.Current;
            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void CurrentShouldThrowWithoutSelector()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .Build();
            Assert.ThrowsException<InvalidOperationException>(() => registry.Current);
        }

        [TestMethod]
        public void TestGetThemeByName()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            ITheme? theme = registry.Get("DARK_HIGH_CONTRAST");
            Assert.IsNotNull(theme);
            Assert.AreEqual(ThemeCapabilities.DarkMode, theme.Capabilities & ThemeCapabilities.DarkMode);
            Assert.AreEqual(ThemeCapabilities.HighContrast, theme.Capabilities & ThemeCapabilities.HighContrast);
        }

        [TestMethod]
        public void TestListThemes()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            IList<ITheme> themes = registry.List();
            Assert.IsTrue(themes.Count > 0);
        }
    }
}