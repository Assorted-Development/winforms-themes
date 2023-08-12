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
            var registry = ThemeRegistryHolder.GetBuilder().EnableLogging(LoggerFactory)
                            .AddThemePlugin<Button>(new ThemePlugin())
                            .Build();
            Assert.AreEqual(1, registry.Get()?.ThemePlugins?.Count);
            Assert.AreEqual(typeof(ThemePlugin), registry.Get()?.ThemePlugins?[typeof(Button)]?.GetType());
        }

        [TestMethod]
        public void AddingThemePluginTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder().EnableLogging(LoggerFactory)
                            .AddThemePlugin<Button>(new ThemePlugin());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddThemePlugin<Button>(new ThemePlugin()));
        }

        [TestMethod]
        public void AddingThemeTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder().EnableLogging(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddTheme(new DefaultDarkTheme()));
        }

        [TestMethod]
        public void CallingWithThemesTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder().EnableLogging(LoggerFactory)
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme())
                            .FinishThemeList();
            Assert.ThrowsException<InvalidOperationException>(() => registry.WithThemes());
        }

        [TestMethod]
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            var registry = ThemeRegistryHolder.GetBuilder().EnableLogging(LoggerFactory)
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
            Assert.AreEqual(0, registry.Get(DefaultDarkTheme.THEME_NAME)?.AdvancedCapabilities?.Count);
        }

        private class ThemePlugin : IThemePlugin
        {
            public void Apply(Control control, AbstractTheme theme)
            { }
        }
    }
}