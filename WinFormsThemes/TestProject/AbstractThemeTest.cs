using System.Windows.Forms;
using WinFormsThemes;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class AbstractThemeTest : AbstractTestClass
    {
        [TestMethod]
        public void PluginShouldBeCalledForExactType()
        {
            ThemePlugin plugin = new();
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            using Button button = new();
            registry.GetTheme()?.Apply(button);
            Assert.IsTrue(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForDifferentType()
        {
            ThemePlugin plugin = new();
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            using Form form = new();
            registry.GetTheme()?.Apply(form);
            Assert.IsFalse(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForSubType()
        {
            ThemePlugin plugin = new();
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            using MyCustomButton button = new();
            registry.GetTheme()?.Apply(button);
            Assert.IsFalse(plugin.WasCalled);
        }

        private class MyCustomButton : Button
        { }

        private class ThemePlugin : IThemePlugin
        {
            public bool WasCalled { get; private set; }

            public void Apply(Control control, AbstractTheme theme)
            {
                WasCalled = true;
            }
        }
    }
}
