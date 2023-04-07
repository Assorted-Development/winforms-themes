using de.mfbot.MFBot_NG.Basisbibliothek;
using MFBot_1701_E.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// abstract class for Dark Themes
    /// </summary>
    public abstract class AbstractTheme : ITheme
    {
        /// <summary>
        /// the name of the theme
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        public abstract ThemeCapabilities Capabilities { get; }

        protected abstract Color ControlBackColor { get; }
        protected abstract Color ControlSuccessBackColor { get; }
        protected abstract Color ControlWarningBackColor { get; }
        protected abstract Color ControlErrorBackColor { get; }
        protected abstract Color ControlForeColor { get; }
        protected abstract Color ControlSuccessForeColor { get; }
        protected abstract Color ControlWarningForeColor { get; }
        protected abstract Color ControlErrorForeColor { get; }
        protected virtual Color TableBackColor => ControlBackColor;
        protected virtual Color TableHeaderBackColor => TableBackColor;
        protected virtual Color TableHeaderForeColor => ControlForeColor;
        protected virtual Color TableCellBackColor => TableBackColor;
        protected virtual Color TableCellForeColor => ControlForeColor;
        public void Apply(Form form)
        {
            form.SuspendLayout();

            DarkTitleBar.UseImmersiveDarkMode(form.Handle, Capabilities.HasFlag(ThemeCapabilities.DarkMode));

            Apply((Control)form);
            if (form.MdiChildren.Length > 0)
            {
                foreach (var children in form.MdiChildren)
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
            control.BackColor = GetBackgroundColorForStyle(options);
            control.ForeColor = GetForgroundColorForStyle(options, !control.Enabled);
            if (control is TreeView tv)
            {
                ApplyTreeView(tv);
            }
            if (control is StylableButton sb)
            {
                sb.EnabledBackColor = GetBackgroundColorForStyle(options);
                sb.EnabledForeColor = GetForgroundColorForStyle(options, false);
                sb.DisabledBackColor = GetBackgroundColorForStyle(options);
                sb.DisabledForeColor = GetForgroundColorForStyle(options, true);
            }
            if (control is StyleableDateTimePicker dtp)
            {
                dtp.EnabledBackColor = GetBackgroundColorForStyle(options);
                dtp.EnabledForeColor = GetForgroundColorForStyle(options, false);
                dtp.DisabledBackColor = GetBackgroundColorForStyle(options);
                dtp.DisabledForeColor = GetForgroundColorForStyle(options, true);
            }
            if (control is DataGridView dgv)
            {
                ApplyDataGridView(dgv);
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
        private Color GetForgroundColorForStyle(ThemeOptions options, bool disabled)
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
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.BackColor = TableCellBackColor;
                col.DefaultCellStyle.ForeColor = TableCellForeColor;
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
            tn.ForeColor = GetForgroundColorForStyle(ThemeOptions.None, false);
            foreach (TreeNode child in tn.Nodes)
            {
                ApplyTreeNode(child);
            }
        }
    }
}
