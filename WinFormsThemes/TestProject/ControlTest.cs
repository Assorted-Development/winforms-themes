using StylableWinFormsControls;
using System.Windows.Forms;
using WinFormsThemes;

namespace TestProject
{
    [TestClass]
    public class ControlTest : AbstractTestClass
    {
        [TestMethod]
        public void TestControlButton()
        {
            testControl(new Button() { Text = "Click me" });
        }

        [TestMethod]
        public void TestControlDataGridView()
        {
            DataGridView dtaGridView = new();
            dtaGridView.Columns.Add("Test", "Test");
            dtaGridView.Rows.Add("Test");
            testControl(dtaGridView);
        }

        [TestMethod]
        public void TestControlMdiForm()
        {
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .Build();

            using Form parent = new() { IsMdiContainer = true };
            using Form child = new() { MdiParent = parent };

            parent.Show();
            testWithTheme(parent, registry.Get(ThemeCapabilities.LightMode));
            testWithTheme(parent, registry.Get(ThemeCapabilities.DarkMode));
            testWithTheme(parent, registry.Get(ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast));
            parent.Close();
        }

        [TestMethod]
        public void TestControlStylableButton()
        {
            testControl(new StylableButton() { Text = "Click me" });
        }

        [TestMethod]
        public void TestControlStylableCheckBox()
        {
            testControl(new StylableCheckBox() { Text = "Check me" });
        }

        [TestMethod]
        public void TestControlStylableComboBox()
        {
            StylableComboBox combobox = new();
            combobox.Items.Add("Item 1");
            combobox.Items.Add("Item 2");
            combobox.Items.Add("Item 3");
            testControl(combobox);
        }

        [TestMethod]
        public void TestControlStylableDateTimePicker()
        {
            testControl(new StylableDateTimePicker() { Value = DateTime.Now });
        }

        [TestMethod]
        public void TestControlStylableLabel()
        {
            testControl(new StylableLabel() { Text = "Test" });
            testControl(new StylableLabel() { Text = "Hint" }, ThemeOptions.Hint);
            testControl(new StylableLabel() { Text = "Success" }, ThemeOptions.Success);
            testControl(new StylableLabel() { Text = "Warning" }, ThemeOptions.Warning);
            testControl(new StylableLabel() { Text = "Error" }, ThemeOptions.Error);
        }

        [TestMethod]
        public void TestControlStylableListView()
        {
            StylableListView listView = new();
            listView.Items.Add("Test");
            testControl(listView);
        }

        [TestMethod]
        public void TestControlStylableTabControl()
        {
            StylableTabControl tabControl = new();
            tabControl.TabPages.Add("Test 1");
            tabControl.TabPages.Add("Test 2");
            testControl(tabControl);
        }

        [TestMethod]
        public void TestControlStylableTextBox()
        {
            testControl(new StylableTextBox() { PlaceholderText = "Test" });
        }

        [TestMethod]
        public void TestControlToolStrip()
        {
            ToolStripContainer toolStripContainer = new();
            ToolStrip toolstrip = new();
            toolstrip.Items.Add("Test");
            toolstrip.Items.Add("One");
            toolstrip.Items.Add("Two");
            toolstrip.Items.Add("Three");
            toolStripContainer.TopToolStripPanel.Controls.Add(toolstrip);
            testControl(toolStripContainer);
            toolstrip.TextDirection = ToolStripTextDirection.Vertical90;
            testControl(toolStripContainer);
        }

        [TestMethod]
        public void TestControlTreeView()
        {
            TreeView treeView = new();
            treeView.Nodes.Add("Test");
            treeView.Nodes[0].Nodes.Add("Child");
            testControl(treeView);
        }

        private static void testWithTheme(Form form, ITheme? theme, ThemeOptions options = ThemeOptions.None)
        {
            if (theme == null)
            {
                return;
            }
            //apply the theme
            theme.Apply(form, options);
            //make sure that the control stays visible visible as some code(e.g. ToolStrip) does not apply
            // the theme until the control is visible and events are processed
            for (int i = 0; i < 10; i++)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        private void testControl(Control c, ThemeOptions options = ThemeOptions.None)
        {
            //create a form and add the control to it
            Form form = new();
            form.Controls.Add(c);
            //create a theme registry
            IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                            .SetLoggerFactory(LoggerFactory)
                            .Build();
            //make sure that the control is visible as some code(e.g. ToolStrip) does not apply
            // the theme until the control is visible
            form.Show();
            testWithTheme(form, registry.Get(ThemeCapabilities.LightMode), options);
            testWithTheme(form, registry.Get(ThemeCapabilities.DarkMode), options);
            testWithTheme(form, registry.Get(ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast), options);
            //close the form but allow the control to be reused
            form.Controls.Remove(c);
            form.Close();
        }
    }
}
