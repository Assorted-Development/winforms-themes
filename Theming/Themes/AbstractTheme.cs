using MFBot_1701_E.CustomControls;
using System.Drawing;
using System.Windows.Forms;
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

        protected virtual Color ControlBorderColor => ControlHighlightColor;
        protected virtual Color ControlBorderLightColor =>
            Color.FromArgb(125, ControlBorderColor);

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
            control.ForeColor = GetForegroundColorForStyle(options, !control.Enabled);

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
            return Color.FromArgb((int)(255 * 0.6), baseColor);
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
