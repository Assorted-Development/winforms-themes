namespace StylableWinFormsControls.Example
{
    partial class FrmDefault
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewGroup listViewGroup1 = new ListViewGroup("ListViewGroup 1", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("ListViewGroup 2", HorizontalAlignment.Left);
            ListViewGroup listViewGroup3 = new ListViewGroup("ListViewGroup", HorizontalAlignment.Left);
            ListViewItem listViewItem1 = new ListViewItem("Content");
            ListViewItem listViewItem2 = new ListViewItem("Content");
            ListViewItem listViewItem3 = new ListViewItem("Content");
            ListViewItem listViewItem4 = new ListViewItem("Content");
            ListViewItem listViewItem5 = new ListViewItem("Content Group 2");
            stylableButton1 = new StylableButton();
            stylableCheckBox1 = new StylableCheckBox();
            stylableComboBox1 = new StylableComboBox();
            stylableDataGridView1 = new StylableDataGridView();
            stylableDateTimePicker1 = new StylableDateTimePicker();
            stylableLabel1 = new StylableLabel();
            stylableListView1 = new StylableListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            stylableTabControl1 = new StylableTabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            stylableTextBox1 = new StylableTextBox();
            lbl_description = new StylableLabel();
            gb_stylabletabcontrol = new GroupBox();
            gb_stylablelistview = new GroupBox();
            gb_stylablebutton = new GroupBox();
            gb_stylablecheckbox = new GroupBox();
            gb_stylableComboBox = new GroupBox();
            gb_stylableDateTimePicker = new GroupBox();
            gb_stylableDataGridView = new GroupBox();
            gb_stylableLabel = new GroupBox();
            gb_stylableTextBox = new GroupBox();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewCheckBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)stylableDataGridView1).BeginInit();
            stylableTabControl1.SuspendLayout();
            gb_stylabletabcontrol.SuspendLayout();
            gb_stylablelistview.SuspendLayout();
            gb_stylablebutton.SuspendLayout();
            gb_stylablecheckbox.SuspendLayout();
            gb_stylableComboBox.SuspendLayout();
            gb_stylableDateTimePicker.SuspendLayout();
            gb_stylableDataGridView.SuspendLayout();
            gb_stylableLabel.SuspendLayout();
            gb_stylableTextBox.SuspendLayout();
            SuspendLayout();
            // 
            // stylableButton1
            // 
            stylableButton1.BorderColor = Color.Black;
            stylableButton1.DisabledBackColor = Color.Gray;
            stylableButton1.DisabledForeColor = Color.Black;
            stylableButton1.EnabledBackColor = Color.White;
            stylableButton1.EnabledForeColor = Color.Black;
            stylableButton1.EnabledHoverColor = Color.LightGray;
            stylableButton1.Location = new Point(6, 22);
            stylableButton1.Name = "stylableButton1";
            stylableButton1.Size = new Size(123, 23);
            stylableButton1.TabIndex = 0;
            stylableButton1.Text = "This is content";
            stylableButton1.UseVisualStyleBackColor = true;
            // 
            // stylableCheckBox1
            // 
            stylableCheckBox1.DisabledForeColor = Color.Empty;
            stylableCheckBox1.Location = new Point(6, 16);
            stylableCheckBox1.Name = "stylableCheckBox1";
            stylableCheckBox1.Size = new Size(125, 24);
            stylableCheckBox1.TabIndex = 1;
            stylableCheckBox1.Text = "This is content";
            stylableCheckBox1.UseVisualStyleBackColor = true;
            // 
            // stylableComboBox1
            // 
            stylableComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            stylableComboBox1.FormattingEnabled = true;
            stylableComboBox1.Items.AddRange(new object[] { "This is content 1", "This is content 2", "This is content 3", "This is content with slightly more length" });
            stylableComboBox1.Location = new Point(6, 22);
            stylableComboBox1.Name = "stylableComboBox1";
            stylableComboBox1.Size = new Size(199, 24);
            stylableComboBox1.TabIndex = 2;
            stylableComboBox1.Text = "This is content";
            // 
            // stylableDataGridView1
            // 
            stylableDataGridView1.AllowUserToOrderColumns = true;
            stylableDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            stylableDataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            stylableDataGridView1.DoubleBuffered = true;
            stylableDataGridView1.EnableFirstColumnGrouping = true;
            stylableDataGridView1.Location = new Point(6, 22);
            stylableDataGridView1.Name = "stylableDataGridView1";
            stylableDataGridView1.RowHeadersVisible = false;
            stylableDataGridView1.RowTemplate.Height = 25;
            stylableDataGridView1.ScrollBars = ScrollBars.None;
            stylableDataGridView1.Size = new Size(759, 150);
            stylableDataGridView1.TabIndex = 3;
            // 
            // stylableDateTimePicker1
            // 
            stylableDateTimePicker1.DisabledBackColor = Color.Gray;
            stylableDateTimePicker1.DisabledForeColor = Color.Black;
            stylableDateTimePicker1.EnabledBackColor = Color.White;
            stylableDateTimePicker1.EnabledForeColor = Color.Black;
            stylableDateTimePicker1.Location = new Point(6, 22);
            stylableDateTimePicker1.Name = "stylableDateTimePicker1";
            stylableDateTimePicker1.Size = new Size(200, 23);
            stylableDateTimePicker1.TabIndex = 4;
            // 
            // stylableLabel1
            // 
            stylableLabel1.AutoSize = true;
            stylableLabel1.DisabledForeColor = Color.Empty;
            stylableLabel1.Location = new Point(6, 22);
            stylableLabel1.Name = "stylableLabel1";
            stylableLabel1.Size = new Size(83, 15);
            stylableLabel1.TabIndex = 5;
            stylableLabel1.Text = "This is content";
            // 
            // stylableListView1
            // 
            stylableListView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            listViewGroup1.Header = "ListViewGroup 1";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup 2";
            listViewGroup2.Name = "listViewGroup2";
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup3";
            stylableListView1.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2, listViewGroup3 });
            stylableListView1.HeaderStyle = ColumnHeaderStyle.None;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup1;
            listViewItem5.Group = listViewGroup2;
            stylableListView1.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4, listViewItem5 });
            stylableListView1.Location = new Point(6, 18);
            stylableListView1.Name = "stylableListView1";
            stylableListView1.Size = new Size(242, 178);
            stylableListView1.TabIndex = 6;
            stylableListView1.UseCompatibleStateImageBehavior = false;
            stylableListView1.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Width = 120;
            // 
            // stylableTabControl1
            // 
            stylableTabControl1.ActiveTabBackgroundColor = SystemColors.Control;
            stylableTabControl1.ActiveTabForegroundColor = SystemColors.ControlText;
            stylableTabControl1.Controls.Add(tabPage1);
            stylableTabControl1.Controls.Add(tabPage2);
            stylableTabControl1.Location = new Point(6, 22);
            stylableTabControl1.Name = "stylableTabControl1";
            stylableTabControl1.SelectedIndex = 0;
            stylableTabControl1.Size = new Size(274, 182);
            stylableTabControl1.TabIndex = 7;
            stylableTabControl1.UseRoundedCorners = false;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(266, 153);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(192, 71);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // stylableTextBox1
            // 
            stylableTextBox1.BorderColor = Color.Blue;
            stylableTextBox1.BorderStyle = BorderStyle.None;
            stylableTextBox1.DelayedTextChangedTimeout = 900;
            stylableTextBox1.ForeColor = Color.Gray;
            stylableTextBox1.PlaceholderText = "Hello, my name is ...";
            stylableTextBox1.PlaceholderForeColor = Color.Gray;
            stylableTextBox1.IsDelayActive = true;
            stylableTextBox1.Location = new Point(6, 22);
            stylableTextBox1.Name = "stylableTextBox1";
            stylableTextBox1.Size = new Size(207, 16);
            stylableTextBox1.TabIndex = 8;
            stylableTextBox1.Text = "Hello, my name is ...";
            stylableTextBox1.ForeColor = Color.Black;
            // 
            // lbl_description
            // 
            lbl_description.AutoSize = true;
            lbl_description.DisabledForeColor = Color.Empty;
            lbl_description.Location = new Point(12, 9);
            lbl_description.Name = "lbl_description";
            lbl_description.Size = new Size(408, 15);
            lbl_description.TabIndex = 9;
            lbl_description.Text = "This form displays all stylable controls in their default state without changes.";
            // 
            // gb_stylabletabcontrol
            // 
            gb_stylabletabcontrol.Controls.Add(stylableTabControl1);
            gb_stylabletabcontrol.Location = new Point(237, 230);
            gb_stylabletabcontrol.Name = "gb_stylabletabcontrol";
            gb_stylabletabcontrol.Size = new Size(286, 218);
            gb_stylabletabcontrol.TabIndex = 11;
            gb_stylabletabcontrol.TabStop = false;
            gb_stylabletabcontrol.Text = "StylableTabControl";
            // 
            // gb_stylablelistview
            // 
            gb_stylablelistview.Controls.Add(stylableListView1);
            gb_stylablelistview.Location = new Point(529, 234);
            gb_stylablelistview.Name = "gb_stylablelistview";
            gb_stylablelistview.Size = new Size(262, 214);
            gb_stylablelistview.TabIndex = 12;
            gb_stylablelistview.TabStop = false;
            gb_stylablelistview.Text = "StylableListView";
            // 
            // gb_stylablebutton
            // 
            gb_stylablebutton.Controls.Add(stylableButton1);
            gb_stylablebutton.Location = new Point(12, 349);
            gb_stylablebutton.Name = "gb_stylablebutton";
            gb_stylablebutton.Size = new Size(217, 59);
            gb_stylablebutton.TabIndex = 13;
            gb_stylablebutton.TabStop = false;
            gb_stylablebutton.Text = "StylableButton";
            // 
            // gb_stylablecheckbox
            // 
            gb_stylablecheckbox.Controls.Add(stylableCheckBox1);
            gb_stylablecheckbox.Location = new Point(610, 456);
            gb_stylablecheckbox.Name = "gb_stylablecheckbox";
            gb_stylablecheckbox.Size = new Size(181, 44);
            gb_stylablecheckbox.TabIndex = 14;
            gb_stylablecheckbox.TabStop = false;
            gb_stylablecheckbox.Text = "StylableCheckBox";
            // 
            // gb_stylableComboBox
            // 
            gb_stylableComboBox.Controls.Add(stylableComboBox1);
            gb_stylableComboBox.Location = new Point(12, 287);
            gb_stylableComboBox.Name = "gb_stylableComboBox";
            gb_stylableComboBox.Size = new Size(217, 56);
            gb_stylableComboBox.TabIndex = 15;
            gb_stylableComboBox.TabStop = false;
            gb_stylableComboBox.Text = "StylableComboBox";
            // 
            // gb_stylableDateTimePicker
            // 
            gb_stylableDateTimePicker.Controls.Add(stylableDateTimePicker1);
            gb_stylableDateTimePicker.Location = new Point(12, 230);
            gb_stylableDateTimePicker.Name = "gb_stylableDateTimePicker";
            gb_stylableDateTimePicker.Size = new Size(217, 51);
            gb_stylableDateTimePicker.TabIndex = 16;
            gb_stylableDateTimePicker.TabStop = false;
            gb_stylableDateTimePicker.Text = "StylableDateTimePicker";
            // 
            // gb_stylableDataGridView
            // 
            gb_stylableDataGridView.Controls.Add(stylableDataGridView1);
            gb_stylableDataGridView.Location = new Point(12, 36);
            gb_stylableDataGridView.Name = "gb_stylableDataGridView";
            gb_stylableDataGridView.Size = new Size(779, 178);
            gb_stylableDataGridView.TabIndex = 17;
            gb_stylableDataGridView.TabStop = false;
            gb_stylableDataGridView.Text = "StylableDataGridView";
            // 
            // gb_stylableLabel
            // 
            gb_stylableLabel.Controls.Add(stylableLabel1);
            gb_stylableLabel.Location = new Point(462, 454);
            gb_stylableLabel.Name = "gb_stylableLabel";
            gb_stylableLabel.Size = new Size(142, 46);
            gb_stylableLabel.TabIndex = 18;
            gb_stylableLabel.TabStop = false;
            gb_stylableLabel.Text = "StylableLabel";
            // 
            // gb_stylableTextBox
            // 
            gb_stylableTextBox.Controls.Add(stylableTextBox1);
            gb_stylableTextBox.Location = new Point(237, 454);
            gb_stylableTextBox.Name = "gb_stylableTextBox";
            gb_stylableTextBox.Size = new Size(219, 46);
            gb_stylableTextBox.TabIndex = 19;
            gb_stylableTextBox.TabStop = false;
            gb_stylableTextBox.Text = "StylableTextBox";
            // 
            // Column1
            // 
            Column1.Frozen = true;
            Column1.HeaderText = "Column1";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Column2";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Column3";
            Column3.Name = "Column3";
            Column3.Resizable = DataGridViewTriState.True;
            Column3.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column4.HeaderText = "Column4";
            Column4.Name = "Column4";
            Column4.Resizable = DataGridViewTriState.True;
            // 
            // FrmDefault
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 510);
            Controls.Add(gb_stylableTextBox);
            Controls.Add(gb_stylableLabel);
            Controls.Add(gb_stylableDataGridView);
            Controls.Add(gb_stylableDateTimePicker);
            Controls.Add(gb_stylableComboBox);
            Controls.Add(gb_stylablecheckbox);
            Controls.Add(gb_stylablebutton);
            Controls.Add(gb_stylablelistview);
            Controls.Add(gb_stylabletabcontrol);
            Controls.Add(lbl_description);
            Name = "FrmDefault";
            Text = "StylableWinFormsControls.Example (Default)";
            ((System.ComponentModel.ISupportInitialize)stylableDataGridView1).EndInit();
            stylableTabControl1.ResumeLayout(false);
            gb_stylabletabcontrol.ResumeLayout(false);
            gb_stylablelistview.ResumeLayout(false);
            gb_stylablebutton.ResumeLayout(false);
            gb_stylablecheckbox.ResumeLayout(false);
            gb_stylableComboBox.ResumeLayout(false);
            gb_stylableDateTimePicker.ResumeLayout(false);
            gb_stylableDataGridView.ResumeLayout(false);
            gb_stylableLabel.ResumeLayout(false);
            gb_stylableLabel.PerformLayout();
            gb_stylableTextBox.ResumeLayout(false);
            gb_stylableTextBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StylableButton stylableButton1;
        private StylableCheckBox stylableCheckBox1;
        private StylableComboBox stylableComboBox1;
        private StylableDataGridView stylableDataGridView1;
        private StylableDateTimePicker stylableDateTimePicker1;
        private StylableLabel stylableLabel1;
        private StylableListView stylableListView1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private StylableTabControl stylableTabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private StylableTextBox stylableTextBox1;
        private StylableLabel lbl_description;
        private TextBox textBox1;
        private GroupBox gb_stylabletabcontrol;
        private GroupBox gb_stylablelistview;
        private GroupBox gb_stylablebutton;
        private GroupBox gb_stylablecheckbox;
        private GroupBox gb_stylableComboBox;
        private GroupBox gb_stylableDateTimePicker;
        private GroupBox gb_stylableDataGridView;
        private GroupBox gb_stylableLabel;
        private GroupBox gb_stylableTextBox;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewCheckBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
    }
}
