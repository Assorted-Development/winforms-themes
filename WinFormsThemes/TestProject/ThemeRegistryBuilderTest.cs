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
                            .AddThemePlugin(new ThemePlugin())
                            .Build();
            Assert.AreEqual(1, registry.GetTheme()?.ThemePlugins?.Count);
            Assert.AreEqual(typeof(ThemePlugin), registry.GetTheme()?.ThemePlugins?[typeof(Button)]?.GetType());
        }

        [TestMethod]
        public void AddingThemePluginTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin(new ThemePlugin());
            InvalidOperationException ex = Assert.ThrowsException<InvalidOperationException>(() => registry.AddThemePlugin(new ThemePlugin()));
            Assert.AreEqual("ThemePlugin for Button already added", ex.Message);
        }

        [TestMethod]
        public void AddingCurrentSelectorTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder()
                            .WithCurrentThemeSelector(registry => registry.GetTheme());
            InvalidOperationException ex = Assert.ThrowsException<InvalidOperationException>(() => registry.WithCurrentThemeSelector(registry => registry.GetTheme()));
            Assert.AreEqual("WithCurrentThemeSelector() can only be called once", ex.Message);
        }

        [TestMethod]
        public void AddingThemeTwiceShouldThrow()
        {
            IThemeRegistryThemeListBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme());
            InvalidOperationException ex = Assert.ThrowsException<InvalidOperationException>(() => registry.AddTheme(new DefaultDarkTheme()));
            Assert.AreEqual($"Theme with name {DefaultDarkTheme.THEME_NAME} already exists", ex.Message);
        }

        [TestMethod]
        public void CallingWithThemesTwiceShouldThrow()
        {
            IThemeRegistryBuilder registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme())
                            .FinishThemeList();
            InvalidOperationException ex = Assert.ThrowsException<InvalidOperationException>(() => registry.WithThemes());
            Assert.AreEqual("WithThemes() can only be called once", ex.Message);
        }

        [TestMethod]
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
            Assert.AreEqual(0, registry.GetTheme(DefaultDarkTheme.THEME_NAME)?.AdvancedCapabilities?.Count);
        }

        private class ThemePlugin : AbstractThemePlugin<Button>
        {
            protected override void ApplyPlugin(Button button, AbstractTheme theme)
            { }
        }
    }
}
