﻿using StylableWinFormsControls;
using WinFormsThemes.Themes.ToolStrip;
using WinFormsThemes.Utilities;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// abstract class for Dark Themes
    /// </summary>
    public abstract class AbstractTheme : ITheme
    {
        /// <summary>
        /// This allows custom themes to add additional tags and capabilities to support more specific theme filtering
        /// </summary>
        public virtual IList<String> AdvancedCapabilities => Array.Empty<String>();

        public abstract Color BackgroundColor { get; }
        public abstract Color ButtonBackColor { get; }
        public abstract Color ButtonForeColor { get; }
        public abstract Color ButtonHoverColor { get; }

        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        public abstract ThemeCapabilities Capabilities { get; }

        public virtual Color ComboBoxItemBackColor => ControlHighlightColor;
        public virtual Color ComboBoxItemHoverColor => GetSoftenedColor(ControlHighlightColor, true);
        public abstract Color ControlBackColor { get; }
        public virtual Color ControlBorderColor => ControlHighlightColor;
        public virtual Color ControlBorderLightColor => ControlBorderColor;
        public abstract Color ControlErrorBackColor { get; }
        public abstract Color ControlErrorForeColor { get; }
        public abstract Color ControlForeColor { get; }
        public abstract Color ControlHighlightColor { get; }
        public virtual Color ControlHighlightDarkColor => GetSoftenedColor(ControlBorderColor);
        public virtual Color ControlHighlightLightColor => GetSoftenedColor(ControlBorderColor, true);
        public abstract Color ControlSuccessBackColor { get; }
        public abstract Color ControlSuccessForeColor { get; }
        public abstract Color ControlWarningBackColor { get; }
        public abstract Color ControlWarningForeColor { get; }
        public abstract Color ForegroundColor { get; }
        public virtual Color ListViewHeaderGroupColor => GetSoftenedColor(ControlHighlightColor, true);

        /// <summary>
        /// the name of the theme
        /// </summary>
        public abstract string Name { get; }

        public virtual Color TableBackColor => ControlBackColor;
        public virtual Color TableCellBackColor => TableBackColor;
        public virtual Color TableCellForeColor => ControlForeColor;
        public virtual Color TableHeaderBackColor => TableBackColor;
        public virtual Color TableHeaderForeColor => ControlForeColor;
        public virtual Color TableSelectionBackColor => ControlHighlightColor;

        /// <summary>
        /// supports styling of custom controls without reimplementing the whole theme
        /// </summary>
        public IDictionary<Type, IThemePlugin> ThemePlugins { get; set; } = new Dictionary<Type, IThemePlugin>();

        public void Apply(Form form)
        {
            form.SuspendLayout();

            DarkWindowsTheme.UseImmersiveDarkMode(form.Handle, Capabilities.HasFlag(ThemeCapabilities.DarkMode));
            DarkWindowsTheme.UseDarkThemeVisualStyle(form.Handle, Capabilities.HasFlag(ThemeCapabilities.DarkMode));

            Apply((Control)form);
            if (form.MdiChildren.Length > 0)
            {
                foreach (Form children in form.MdiChildren)
                {
                    Apply(children);
                }
            }
            form.ResumeLayout();
        }

        public void Apply(Control control)
        {
            Apply(control, ThemeOptions.None);
        }

        public void Apply(Control control, ThemeOptions options)
        {
            DarkWindowsTheme.UseDarkThemeVisualStyle(control.Handle, Capabilities.HasFlag(ThemeCapabilities.DarkMode));
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;

            control.BackColor = GetBackgroundColorForStyle(options);

            // always assume disabled==false here since most controls don't support ForeColor on disabled states
            // and have to be set separately
            control.ForeColor = GetForegroundColorForStyle(options, false);

            Type t = control.GetType();
            this.ThemePlugins.TryGetValue(t, out IThemePlugin? plugin);
            if (plugin != null)
            {
                //TODO: does not currently support subclasses of registered types
                plugin.Apply(control, this);
                //Plugins should be able to override OOTB logic so we skip every logic when a plugin is found
                return;
            }

            if (control is Form form)
            {
                form.BackColor = BackgroundColor;
                form.ForeColor = ForegroundColor;
            }

            if (control is TreeView tv)
            {
                ApplyTreeView(tv);
            }

            if (control is StylableButton sb)
            {
                sb.EnabledBackColor = ButtonBackColor;
                sb.EnabledForeColor = ButtonForeColor;
                sb.EnabledHoverColor = ButtonHoverColor;
                sb.BorderColor = ControlBorderColor;

                sb.DisabledBackColor = ButtonHoverColor;
                sb.DisabledForeColor = GetForegroundColorForStyle(options, true);
            }

            if (control is StylableDateTimePicker dtp)
            {
                dtp.EnabledBackColor = GetBackgroundColorForStyle(options);
                dtp.EnabledForeColor = GetForegroundColorForStyle(options, false);
                dtp.DisabledBackColor = GetBackgroundColorForStyle(options);
                dtp.DisabledForeColor = GetForegroundColorForStyle(options, true);
            }
            if (control is DataGridView dgv)
            {
                ApplyDataGridView(dgv);
            }

            if (control is System.Windows.Forms.ToolStrip ts)
            {
                ts.Renderer = new ThemedToolStripRenderer(
                    new ThemedColorTable(
                        Color.Transparent, ControlBorderLightColor, ButtonHoverColor, ControlHighlightColor, ControlBackColor),
                    ButtonForeColor,
                    GetForegroundColorForStyle(options, true))
                {
                    RoundedEdges = false
                };
            }

            if (control is StylableTextBox stb)
            {
                //it is okay to run this line multiple times as the eventhandler will detect this and ignore
                //subsequent calls
                stb.HintActiveChanged += (sender, e) => { if (sender != null) Apply((Control)sender); };
                if (stb.IsHintActive && options != ThemeOptions.Hint)
                {
                    Apply(stb, ThemeOptions.Hint);
                    return;
                }
                stb.BorderColor = ControlBorderColor;
            }

            if (control is StylableTabControl stc)
            {
                stc.BackgroundColor = ControlBackColor;
                stc.ActiveTabBackgroundColor = ControlHighlightColor;
                stc.ActiveTabForegroundColor = ControlForeColor;
            }

            if (control is StylableLabel stl)
            {
                stl.DisabledForeColor = GetForegroundColorForStyle(options, true);
            }

            if (control is StylableListView slv)
            {
                slv.GroupHeaderForeColor = ListViewHeaderGroupColor;
                slv.GroupHeaderBackColor = Color.Transparent;
                slv.SelectedItemBackColor = ControlHighlightColor;
                slv.SelectedItemForeColor = ControlForeColor;
            }

            if (control is StylableCheckBox scb)
            {
                scb.DisabledForeColor = GetForegroundColorForStyle(options, true);
            }

            if (control is StylableComboBox scbx)
            {
                scbx.ForeColor = ControlForeColor;
                scbx.BackColor = ComboBoxItemBackColor;
                scbx.ItemHoverColor = ComboBoxItemHoverColor;
                scbx.BorderColor = ControlBorderColor;
            }

            foreach (Control child in control.Controls)
            {
                Apply(child);
            }
        }

        /// <summary>
        /// Gets a weaker/softer version of the color passed.
        /// </summary>
        /// <param name="baseColor">Color to weaken</param>
        /// <param name="switchDarkAndLight">If true, a bright color will be made softly brighter, otherwise darker.</param>
        /// <returns>Softened color</returns>
        /// <remarks>
        /// This should primarily thought of as helper function to use the same colors and modify them
        /// dependent on dark/light theme.
        /// </remarks>
        protected static Color GetSoftenedColor(Color baseColor, bool switchDarkAndLight = false)
        {
            // HSL lightness value 0 = black, 1 = white
            if (baseColor.GetBrightness() < 0.5 || switchDarkAndLight)
            {
                return Color.FromArgb(
                    baseColor.A,
                    Math.Min(255, baseColor.R > 10 ? (int)(baseColor.R * 1.3) : 100),
                    Math.Min(255, baseColor.G > 10 ? (int)(baseColor.G * 1.3) : 100),
                    Math.Min(255, baseColor.B > 10 ? (int)(baseColor.B * 1.3) : 100));
            }

            return Color.FromArgb(baseColor.A, baseColor.R / 2, baseColor.G / 2, baseColor.B / 2);
        }

        private void ApplyDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = TableHeaderBackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TableHeaderForeColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = TableCellBackColor;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TableCellForeColor;

            dgv.BackgroundColor = TableBackColor;
            dgv.GridColor = ControlBorderColor;

            dgv.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single;
            dgv.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dgv.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single;

            dgv.AdvancedColumnHeadersBorderStyle.Bottom =
                Capabilities.HasFlag(ThemeCapabilities.DarkMode)
                    ? DataGridViewAdvancedCellBorderStyle.InsetDouble
                    : DataGridViewAdvancedCellBorderStyle.OutsetPartial;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.BackColor = TableCellBackColor;
                col.DefaultCellStyle.ForeColor = TableCellForeColor;
                col.DefaultCellStyle.SelectionBackColor = TableSelectionBackColor;
            }
        }

        private void ApplyTreeNode(TreeNode tn)
        {
            tn.BackColor = GetBackgroundColorForStyle(ThemeOptions.None);
            tn.ForeColor = GetForegroundColorForStyle(ThemeOptions.None, false);
            foreach (TreeNode child in tn.Nodes)
            {
                ApplyTreeNode(child);
            }
        }

        private void ApplyTreeView(TreeView tv)
        {
            foreach (TreeNode child in tv.Nodes)
            {
                ApplyTreeNode(child);
            }
        }

        private Color GetBackgroundColorForStyle(ThemeOptions options)
        {
            return options switch
            {
                ThemeOptions.Success => ControlSuccessBackColor,
                ThemeOptions.Warning => ControlWarningBackColor,
                ThemeOptions.Error => ControlErrorBackColor,
                _ => ControlBackColor,
            };
        }

        private Color GetForegroundColorForStyle(ThemeOptions options, bool disabled)
        {
            var baseColor = options switch
            {
                ThemeOptions.Success => ControlSuccessForeColor,
                ThemeOptions.Warning => ControlWarningForeColor,
                ThemeOptions.Error => ControlErrorForeColor,
                ThemeOptions.Hint => ControlForeColor,
                _ => ControlForeColor,
            };

            // HSL lightness value 0 = black, 1 = white
            if (disabled)
            {
                return GetSoftenedColor(baseColor);
            }
            return Color.FromArgb((int)(255 * 0.6), baseColor);
        }
    }
}