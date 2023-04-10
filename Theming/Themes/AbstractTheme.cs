using System;
using MFBot_1701_E.CustomControls;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MFBot_1701_E.Theming.Themes.ToolStrip;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// abstract class for Dark Themes
    /// </summary>
    public abstract partial class AbstractTheme : ITheme
    {
        /// <summary>
        /// the name of the theme
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        public abstract ThemeCapabilities Capabilities { get; }

        protected abstract Color BackgroundColor { get; }
        protected abstract Color ForegroundColor { get; }

        protected abstract Color ButtonForeColor { get; }
        protected abstract Color ButtonBackColor { get; }
        protected abstract Color ButtonHoverColor { get; }

        protected abstract Color ControlForeColor { get; }
        protected abstract Color ControlBackColor { get; }
        protected abstract Color ControlSuccessBackColor { get; }
        protected abstract Color ControlWarningBackColor { get; }
        protected abstract Color ControlErrorBackColor { get; }
        protected abstract Color ControlHighlightColor { get; }

        protected abstract Color ControlSuccessForeColor { get; }
        protected abstract Color ControlWarningForeColor { get; }
        protected abstract Color ControlErrorForeColor { get; }

        protected virtual Color TableBackColor => ControlBackColor;
        protected virtual Color TableHeaderBackColor => TableBackColor;
        protected virtual Color TableHeaderForeColor => ControlForeColor;
        protected virtual Color TableSelectionBackColor => ControlHighlightColor;
        protected virtual Color TableCellBackColor => TableBackColor;
        protected virtual Color TableCellForeColor => ControlForeColor;

        protected virtual Color ListViewHeaderGroupColor => GetSoftenedColor(ControlHighlightColor, true);

        protected virtual Color ControlHighlightLightColor => GetSoftenedColor(ControlBorderColor, true);
        protected virtual Color ControlHighlightDarkColor => GetSoftenedColor(ControlBorderColor);
        protected virtual Color ControlBorderColor => ControlHighlightColor;
        protected virtual Color ControlBorderLightColor => ControlBorderColor;

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

            if (control is StyleableDateTimePicker dtp)
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

            //TODO: Determine how to make external components stylable despite this being an external library itself
            if (control is ObjectListView olv)
            {
                olv.HeaderUsesThemes = false;
                HeaderFormatStyle headerFormatStyle = new();
                headerFormatStyle.SetBackColor(TableHeaderBackColor);
                headerFormatStyle.SetForeColor(TableHeaderForeColor);

                olv.HeaderFormatStyle = headerFormatStyle;
                olv.AlternateRowBackColor = ControlHighlightDarkColor;
            }

            foreach (Control child in control.Controls)
            {
                Apply(child);
            }
        }
        private Color GetBackgroundColorForStyle(ThemeOptions options)
        {
            switch (options)
            {
                case ThemeOptions.Success:
                    return ControlSuccessBackColor;
                case ThemeOptions.Warning:
                    return ControlWarningBackColor;
                case ThemeOptions.Error:
                    return ControlErrorBackColor;
                default:
                    return ControlBackColor;
            }
        }
        private Color GetForegroundColorForStyle(ThemeOptions options, bool disabled)
        {
            double opacity = disabled ? 0.38 : 1.0;
            Color baseColor;
            switch (options)
            {
                case ThemeOptions.Success:
                    baseColor = ControlSuccessForeColor;
                    break;
                case ThemeOptions.Warning:
                    baseColor = ControlWarningForeColor;
                    break;
                case ThemeOptions.Error:
                    baseColor = ControlErrorForeColor;
                    break;
                case ThemeOptions.Hint:
                    opacity = 0.6;
                    baseColor = ControlForeColor;
                    break;
                default:
                    baseColor = ControlForeColor;
                    break;
            }

            // HSL lightness value 0 = black, 1 = white
            if (disabled)
            {
                return GetSoftenedColor(baseColor);
            }
            return Color.FromArgb((int)(255 * 0.6), baseColor);
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
        private Color GetSoftenedColor(Color baseColor, bool switchDarkAndLight = false)
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
        private void ApplyTreeView(TreeView tv)
        {
            foreach (TreeNode child in tv.Nodes)
            {
                ApplyTreeNode(child);
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
    }
}
