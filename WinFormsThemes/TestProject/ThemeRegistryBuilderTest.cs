using System.Windows.Forms;
using WinFormsThemes;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class ThemeRegistryBuilderTest : AbstractTestClass
    {
        [TestMethod]
        public void AddingThemePluginsShouldWork()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(new ThemePlugin())
                            .Build();
            Assert.AreEqual(1, registry.GetTheme()?.ThemePlugins?.Count);
            Assert.AreEqual(typeof(ThemePlugin), registry.GetTheme()?.ThemePlugins?[typeof(Button)]?.GetType());
        }

        [TestMethod]
        public void AddingThemePluginTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(new ThemePlugin());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddThemePlugin<Button>(new ThemePlugin()));
        }

        [TestMethod]
        public void AddingCurrentSelectorTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder()
                            .WithCurrentThemeSelector(registry => registry.GetTheme());
            Assert.ThrowsException<InvalidOperationException>(() => registry.WithCurrentThemeSelector(registry => registry.GetTheme()));
        }

        [TestMethod]
        public void AddingThemeTwiceShouldThrow()
        {
            IThemeRegistryThemeListBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddTheme(new DefaultDarkTheme()));
        }

        [TestMethod]
        public void CallingWithThemesTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme())
                            .FinishThemeList();
            Assert.ThrowsException<InvalidOperationException>(() => registry.WithThemes());
        }

        [TestMethod]
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
            Assert.AreEqual(0, registry.GetTheme(DefaultDarkTheme.THEME_NAME)?.AdvancedCapabilities?.Count);
        }

        private class ThemePlugin : IThemePlugin
        {
            public void Apply(Control control, AbstractTheme theme)
            { }
        }
    }
}