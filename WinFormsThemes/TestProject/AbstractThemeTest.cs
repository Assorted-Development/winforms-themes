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
                            .AddThemePlugin(plugin)
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
                            .AddThemePlugin(plugin)
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
                            .AddThemePlugin(plugin)
                            .Build();
            using MyCustomButton button = new();
            registry.GetTheme()?.Apply(button);
            Assert.IsFalse(plugin.WasCalled);
        }

        private class MyCustomButton : Button
        { }

        private class ThemePlugin : AbstractThemePlugin<Button>
        {
            public bool WasCalled { get; private set; }

            protected override void ApplyPlugin(Button button, AbstractTheme theme)
            {
                WasCalled = true;
            }
        }
    }
}
