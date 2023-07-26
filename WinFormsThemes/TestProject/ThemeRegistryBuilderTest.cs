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
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
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