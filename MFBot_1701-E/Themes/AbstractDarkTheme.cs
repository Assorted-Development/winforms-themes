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
        protected abstract Color ControlForeColor { get; }
        protected virtual Color TableBackColor => ControlBackColor;
        protected virtual Color TableHeaderBackColor => TableBackColor;
        protected virtual Color TableHeaderForeColor => ControlForeColor;
        protected virtual Color TableCellBackColor => TableBackColor;
        protected virtual Color TableCellForeColor => ControlForeColor;
        public void Apply(Form form)
        {
            DarkTitleBar.UseImmersiveDarkMode(form.Handle, true);
            Apply((Control)form);
        }

        public void Apply(Control control)
        {
            control.BackColor = ControlBackColor;
            control.ForeColor = ControlForeColor;
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
