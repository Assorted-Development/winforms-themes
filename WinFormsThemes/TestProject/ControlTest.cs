using StylableWinFormsControls;
using System.Windows.Forms;
using WinFormsThemes;

namespace TestProject
{
    [TestCategory("LONG_RUNNING")]
    [TestClass]
    public class ControlTest
    {
        [TestMethod]
        public void TestControlButton()
        {
            TestControl(new Button() { Text = "Click me" });
        }

        [TestMethod]
        public void TestControlDataGridView()
        {
            var dtaGridView = new DataGridView();
            dtaGridView.Columns.Add("Test", "Test");
            dtaGridView.Rows.Add("Test");
            TestControl(dtaGridView);
        }

        [TestMethod]
        public void TestControlMdiForm()
        {
            var registry = ThemeRegistryHolder.GetBuilder()
                            .Build();
            var parent = new Form
            {
                IsMdiContainer = true
            };
            var child = new Form
            {
                MdiParent = parent
            };
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
            var combobox = new StylableComboBox();
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
            var listView = new StylableListView();
            listView.Items.Add("Test");
            TestControl(listView);
        }

        [TestMethod]
        public void TestControlStylableTabControl()
        {
            var tabControl = new StylableTabControl();
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
            var toolStripContainer = new ToolStripContainer();
            var toolstrip = new ToolStrip();
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
            var treeView = new TreeView();
            treeView.Nodes.Add("Test");
            treeView.Nodes[0].Nodes.Add("Child");
            TestControl(treeView);
        }

        private static void TestControl(Control c, ThemeOptions options = ThemeOptions.None)
        {
            //create a form and add the control to it
            var form = new Form();
            form.Controls.Add(c);
            //create a theme registry
            var registry = ThemeRegistryHolder.GetBuilder()
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
    }
}