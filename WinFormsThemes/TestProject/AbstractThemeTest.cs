using StylableWinFormsControls;
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

        [TestMethod]
        public void TestControlButton()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var button = new Button();
            registry.Current?.Apply(button);
        }

        [TestMethod]
        public void TestControlDataGridView()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var dtaGridView = new DataGridView();
            dtaGridView.Columns.Add("Test", "Test");
            dtaGridView.Rows.Add("Test");
            registry.Current?.Apply(dtaGridView);
        }

        [TestMethod]
        public void TestControlMdiForm()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var parent = new Form();
            parent.IsMdiContainer = true;
            var child = new Form();
            child.MdiParent = parent;
            registry.Current?.Apply(parent);
        }

        [TestMethod]
        public void TestControlStylableButton()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var button = new StylableButton();
            registry.Current?.Apply(button);
        }

        [TestMethod]
        public void TestControlStylableCheckBox()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var checkbox = new StylableCheckBox();
            checkbox.Text = "Test";
            registry.Current?.Apply(checkbox);
        }

        [TestMethod]
        public void TestControlStylableComboBox()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var combobox = new StylableComboBox();
            combobox.Items.Add("Item 1");
            combobox.Items.Add("Item 2");
            combobox.Items.Add("Item 3");
            registry.Current?.Apply(combobox);
        }

        [TestMethod]
        public void TestControlStylableDateTimePicker()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var dateTimePicker = new StylableDateTimePicker();
            registry.Current?.Apply(dateTimePicker);
        }

        [TestMethod]
        public void TestControlStylableLabel()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var label = new StylableLabel();
            label.Text = "Test";
            registry.Current?.Apply(label);
        }

        [TestMethod]
        public void TestControlStylableListView()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var listView = new StylableListView();
            listView.Items.Add("Test");
            registry.Current?.Apply(listView);
        }

        [TestMethod]
        public void TestControlStylableTabControl()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var tabControl = new StylableTabControl();
            tabControl.TabPages.Add("Test");
            registry.Current?.Apply(tabControl);
        }

        [TestMethod]
        public void TestControlStylableTextBox()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var textbox = new StylableTextBox();
            registry.Current?.Apply(textbox);
        }

        [TestMethod]
        public void TestControlToolStrip()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var toolstrip = new ToolStrip();
            toolstrip.Items.Add("Test");
            registry.Current?.Apply(toolstrip);
        }

        [TestMethod]
        public void TestControlTreeView()
        {
            var registry = IThemeRegistry.BUILDER
                            .Build();
            var treeView = new TreeView();
            treeView.Nodes.Add("Test");
            treeView.Nodes[0].Nodes.Add("Child");
            registry.Current?.Apply(treeView);
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