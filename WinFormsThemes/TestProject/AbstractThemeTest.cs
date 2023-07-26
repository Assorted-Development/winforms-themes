using System.Windows.Forms;
using WinFormsThemes;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class AbstractThemeTest
    {
        [TestMethod]
        public void PluginShouldBeCalledForExactType()
        {
            var plugin = new ThemePlugin();
            var registry = IThemeRegistry.BUILDER
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var button = new Button();
            registry.Current?.Apply(button);
            Assert.IsTrue(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForDifferentType()
        {
            var plugin = new ThemePlugin();
            var registry = IThemeRegistry.BUILDER
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var form = new Form();
            registry.Current?.Apply(form);
            Assert.IsFalse(plugin.WasCalled);
        }

        [TestMethod]
        public void PluginShouldNotBeCalledForSubType()
        {
            var plugin = new ThemePlugin();
            var registry = IThemeRegistry.BUILDER
                            .AddThemePlugin<Button>(plugin)
                            .Build();
            var button = new MyCustomButton();
            registry.Current?.Apply(button);
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