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
            var plugin = new ThemePlugin();
            var registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var button = new Button();
            registry.Get()?.Apply(button);
            Assert.IsTrue(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForDifferentType()
        {
            var plugin = new ThemePlugin();
            var registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var form = new Form();
            registry.Get()?.Apply(form);
            Assert.IsFalse(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForSubType()
        {
            var plugin = new ThemePlugin();
            var registry = ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory)
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var button = new MyCustomButton();
            registry.Get()?.Apply(button);
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