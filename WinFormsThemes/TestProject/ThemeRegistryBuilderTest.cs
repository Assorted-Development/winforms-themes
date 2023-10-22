using System.Windows.Forms;
using TestProject.Properties;
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
            var registry = ThemeRegistryHolder.GetBuilder()
                            .AddThemePlugin<Button>(new ThemePlugin())
                            .Build();
            Assert.AreEqual(1, registry.Get()?.ThemePlugins?.Count);
            Assert.AreEqual(typeof(ThemePlugin), registry.Get()?.ThemePlugins?[typeof(Button)]?.GetType());
        }

        [TestMethod]
        public void AddingThemePluginTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .AddThemePlugin<Button>(new ThemePlugin());
            var ex = Assert.ThrowsException<InvalidOperationException>(() => registry.AddThemePlugin<Button>(new ThemePlugin()));
            Assert.AreEqual("ThemePlugin for Button already added", ex.Message);
        }

        [TestMethod]
        public void AddingCurrentSelectorTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .WithCurrentThemeSelector(registry => registry.Get());
            var ex = Assert.ThrowsException<InvalidOperationException>(() => registry.WithCurrentThemeSelector(registry => registry.Get()));
            Assert.AreEqual("WithCurrentThemeSelector() can only be called once", ex.Message);
        }

        [TestMethod]
        public void AddingThemeTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme());
            var ex = Assert.ThrowsException<InvalidOperationException>(() => registry.AddTheme(new DefaultDarkTheme()));
            Assert.AreEqual($"Theme with name {DefaultDarkTheme.THEME_NAME} already exists", ex.Message);
        }

        [TestMethod]
        public void CallingWithThemesTwiceShouldThrow()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .WithThemes()
                                .AddTheme(new DefaultDarkTheme())
                            .FinishThemeList();
            var ex = Assert.ThrowsException<InvalidOperationException>(() => registry.WithThemes());
            Assert.AreEqual("WithThemes() can only be called once", ex.Message);
        }

        [TestMethod]
        public void DefaultsShouldBeAddedWhenNotSet()
        {
            Directory.CreateDirectory("themes");
            File.WriteAllText("themes\\test.theme.json", Resources.CONFIG_THEMING_THEME_FILE_TEST_theme);
            var registry = ThemeRegistryHolder.GetBuilder()
                            .Build();
            Assert.IsTrue(registry.ListNames().Contains(DefaultDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(DefaultLightTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains(HighContrastDarkTheme.THEME_NAME));
            Assert.IsTrue(registry.ListNames().Contains("resource-file-test"));
            Assert.IsTrue(registry.ListNames().Contains("file-ok-test"));
            Assert.AreEqual(7, registry.List().Count);
        }

        [TestMethod]
        public void VerifyOrder()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .WithThemes()
                                .WithLookup(new SecondThemeLookup())
                                .WithLookup(new FirstThemeLookup())
                                .FinishThemeList()
                            .Build();
            Assert.AreEqual(2, registry.ListNames().Count);
            Assert.AreEqual(DefaultDarkTheme.THEME_NAME, registry.List()[0].Name);
            Assert.AreEqual(DefaultLightTheme.THEME_NAME, registry.List()[1].Name);
        }

        private class FirstThemeLookup : IThemeLookup
        {
            public int Order => Int32.MaxValue;

            public List<ITheme> Lookup()
            {
                return new List<ITheme> { new DefaultDarkTheme() };
            }
        }

        private class SecondThemeLookup : IThemeLookup
        {
            public int Order => Int32.MaxValue - 1;

            public List<ITheme> Lookup()
            {
                return new List<ITheme> { new DefaultLightTheme() };
            }
        }

        private class ThemePlugin : IThemePlugin
        {
            public void Apply(Control control, AbstractTheme theme)
            { }
        }
    }
}