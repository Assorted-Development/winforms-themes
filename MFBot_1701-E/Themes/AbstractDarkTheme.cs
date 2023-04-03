using de.mfbot.MFBot_NG.Basisbibliothek;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFBot_1701_E.Themes
{
    /// <summary>
    /// abstract class for Dark Themes
    /// </summary>
    public abstract class AbstractDarkTheme : ITheme
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
            DarkTitleBar.UseImmersiveDarkMode(form.Handle, true);
            Apply((Control)form);
            if(form.MdiChildren.Length > 0)
            {
                foreach(var children in form.MdiChildren)
                {
                    Apply(children);
                }
            }
        }

        public void Apply(Control control)
        {
            Apply(control, ThemeOptions.None);
        }

        public void Apply(Control control, ThemeOptions options)
        {
            switch (options)
            {
                case ThemeOptions.Success:
                    control.BackColor = ControlSuccessBackColor;
                    control.ForeColor = ControlSuccessForeColor;
                    break;
                case ThemeOptions.Warning:
                    control.BackColor = ControlWarningBackColor;
                    control.ForeColor = ControlWarningForeColor;
                    break;
                case ThemeOptions.Error:
                    control.BackColor = ControlErrorBackColor;
                    control.ForeColor = ControlErrorForeColor;
                    break;
                default:
                    control.BackColor = ControlBackColor;
                    control.ForeColor = ControlForeColor;
                    break;
            }
            if (control is DataGridView dgv)
            {
                Apply(dgv);
            }
            foreach (Control child in control.Controls)
            {
                Apply(child);
            }
        }

        private void Apply(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = TableHeaderBackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TableHeaderForeColor;
            dgv.BackgroundColor = TableBackColor;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.BackColor = TableCellBackColor;
                col.DefaultCellStyle.ForeColor = TableCellForeColor;
            }
        }
    }
}
