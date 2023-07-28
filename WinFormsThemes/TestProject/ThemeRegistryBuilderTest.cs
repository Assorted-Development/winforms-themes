using System.Windows.Forms;
using WinFormsThemes;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class ThemeRegistryBuilderTest
    {
        [TestMethod]
        public void AddingThemePluginsShouldWork()
        {
            var registry = IThemeRegistry.BUILDER
                            .AddThemePlugin<Button>(new ThemePlugin())
                            .Build();
            Assert.AreEqual(1, registry.Current?.ThemePlugins?.Count);
            Assert.AreEqual(typeof(ThemePlugin), registry.Current?.ThemePlugins?[typeof(Button)]?.GetType());
        }

        [TestMethod]
        public void AddingThemePluginTwiceShouldThrow()
        {
            var registry = IThemeRegistry.BUILDER
                            .AddThemePlugin<Button>(new ThemePlugin());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddThemePlugin<Button>(new ThemePlugin()));
        }

        [TestMethod]
        public void AddingThemeTwiceShouldThrow()
        {
            var registry = IThemeRegistry.BUILDER
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme());
            Assert.ThrowsException<InvalidOperationException>(() => registry.AddTheme(new DefaultDarkTheme()));
        }

        [TestMethod]
        public void CallingWithThemesTwiceShouldThrow()
        {
            var registry = IThemeRegistry.BUILDER
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme())
                            .CompleteThemeList();
            Assert.ThrowsException<InvalidOperationException>(() => registry.WithThemes());
        }

        [TestMethod]
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
            Assert.AreEqual(0, registry.Get(DefaultDarkTheme.THEME_NAME)?.AdvancedCapabilities?.Count);
        }

        [TestMethod]
        public void ThemeRegistryHolderShouldBeSet()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            Assert.AreEqual(registry, ThemeRegistryHolder.ThemeRegistry);
        }

        private class ThemePlugin : IThemePlugin
        {
            public void Apply(Control control, AbstractTheme theme)
            { }
        }
    }
}