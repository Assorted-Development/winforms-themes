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
            TestControl(new Button() { Text = "Click me" });
        }

        [TestMethod]
        public void TestControlDataGridView()
        {
            DataGridView dtaGridView = new();
            dtaGridView.Columns.Add("Test", "Test");
            dtaGridView.Rows.Add("Test");
            TestControl(dtaGridView);
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
            TestWithTheme(parent, registry.Get(ThemeCapabilities.LightMode));
            TestWithTheme(parent, registry.Get(ThemeCapabilities.DarkMode));
            TestWithTheme(parent, registry.Get(ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast));
            parent.Close();
        }

        [TestMethod]
        public void TestControlStylableButton()
        {
            TestControl(new StylableButton() { Text = "Click me" });
        }

        [TestMethod]
        public void TestControlStylableCheckBox()
        {
            TestControl(new StylableCheckBox() { Text = "Check me" });
        }

        [TestMethod]
        public void TestControlStylableComboBox()
        {
            StylableComboBox combobox = new();
            combobox.Items.Add("Item 1");
            combobox.Items.Add("Item 2");
            combobox.Items.Add("Item 3");
            TestControl(combobox);
        }

        [TestMethod]
        public void TestControlStylableDateTimePicker()
        {
            TestControl(new StylableDateTimePicker() { Value = DateTime.Now });
        }

        [TestMethod]
        public void TestControlStylableLabel()
        {
            TestControl(new StylableLabel() { Text = "Test" });
            TestControl(new StylableLabel() { Text = "Hint" }, ThemeOptions.Hint);
            TestControl(new StylableLabel() { Text = "Success" }, ThemeOptions.Success);
            TestControl(new StylableLabel() { Text = "Warning" }, ThemeOptions.Warning);
            TestControl(new StylableLabel() { Text = "Error" }, ThemeOptions.Error);
        }

        [TestMethod]
        public void TestControlStylableListView()
        {
            StylableListView listView = new();
            listView.Items.Add("Test");
            TestControl(listView);
        }

        [TestMethod]
        public void TestControlStylableTabControl()
        {
            StylableTabControl tabControl = new();
            tabControl.TabPages.Add("Test 1");
            tabControl.TabPages.Add("Test 2");
            TestControl(tabControl);
        }

        [TestMethod]
        public void TestControlStylableTextBox()
        {
            TestControl(new StylableTextBox() { PlaceholderText = "Test" });
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
            TestControl(toolStripContainer);
            toolstrip.TextDirection = ToolStripTextDirection.Vertical90;
            TestControl(toolStripContainer);
        }

        [TestMethod]
        public void TestControlTreeView()
        {
            TreeView treeView = new();
            treeView.Nodes.Add("Test");
            treeView.Nodes[0].Nodes.Add("Child");
            TestControl(treeView);
        }

        private static void TestWithTheme(Form form, ITheme? theme, ThemeOptions options = ThemeOptions.None)
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

        private void TestControl(Control c, ThemeOptions options = ThemeOptions.None)
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
            TestWithTheme(form, registry.Get(ThemeCapabilities.LightMode), options);
            TestWithTheme(form, registry.Get(ThemeCapabilities.DarkMode), options);
            TestWithTheme(form, registry.Get(ThemeCapabilities.DarkMode | ThemeCapabilities.HighContrast), options);
            //close the form but allow the control to be reused
            form.Controls.Remove(c);
            form.Close();
        }
    }
}
