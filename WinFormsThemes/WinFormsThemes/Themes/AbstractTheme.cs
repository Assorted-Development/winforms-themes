using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StylableWinFormsControls;
using StylableWinFormsControls.Controls;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.ComponentModel;
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
        public virtual IList<string> AdvancedCapabilities => Array.Empty<string>();

        [ExcludeFromCodeCoverage]
        public abstract Color BackgroundColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ButtonBackColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ButtonForeColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ButtonHoverColor { get; }

        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        public abstract ThemeCapabilities Capabilities { get; }

        [ExcludeFromCodeCoverage]
        public virtual Color ComboBoxItemBackColor => ControlHighlightColor;

        [ExcludeFromCodeCoverage]
        public virtual Color ComboBoxItemHoverColor => GetSoftenedColor(ControlHighlightColor, true);

        [ExcludeFromCodeCoverage]
        public abstract Color ControlBackColor { get; }

        [ExcludeFromCodeCoverage]
        public virtual Color ControlBorderColor => ControlHighlightColor;

        [ExcludeFromCodeCoverage]
        public abstract Color ControlErrorBackColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlErrorForeColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlForeColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlHighlightColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlSuccessBackColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlSuccessForeColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlWarningBackColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ControlWarningForeColor { get; }

        [ExcludeFromCodeCoverage]
        public abstract Color ForegroundColor { get; }

        [ExcludeFromCodeCoverage]
        public virtual Color ListViewHeaderGroupColor => GetSoftenedColor(ControlHighlightColor, true);

        /// <summary>
        /// the name of the theme
        /// </summary>
        public abstract string Name { get; }

        [ExcludeFromCodeCoverage]
        public virtual Color TableBackColor => ControlBackColor;

        [ExcludeFromCodeCoverage]
        public virtual Color TableCellBackColor => TableBackColor;

        [ExcludeFromCodeCoverage]
        public virtual Color TableCellForeColor => ControlForeColor;

        [ExcludeFromCodeCoverage]
        public virtual Color TableHeaderBackColor => TableBackColor;

        [ExcludeFromCodeCoverage]
        public virtual Color TableHeaderForeColor => ControlForeColor;

        [ExcludeFromCodeCoverage]
        public virtual Color TableSelectionBackColor => ControlHighlightColor;

        /// <summary>
        /// supports styling of custom controls without reimplementing the whole theme
        /// </summary>
        public IDictionary<Type, IThemePlugin> ThemePlugins { get; set; } = new Dictionary<Type, IThemePlugin>();

        /// <summary>
        /// the logger to use
        /// </summary>
        protected ILogger<ITheme> Logger { get; private set; } = new Logger<ITheme>(new NullLoggerFactory());

        public void Apply(Form form)
        {
            Apply(form, ThemeOptions.None);
        }

        public void Apply(Form form, ThemeOptions options)
        {
            ArgumentNullException.ThrowIfNull(form);
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
            ArgumentNullException.ThrowIfNull(control);

            DarkWindowsTheme.UseDarkThemeVisualStyle(control.Handle, Capabilities.HasFlag(ThemeCapabilities.DarkMode));
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.Professional;

            // some specific controls provide more specific setters (like StylableTextBox with HintForeColor and TextForeColor).
            // to not override the color everytime, we ignore non-browsable setters
            setIfBrowsable(control, "BackColor", () => control.BackColor = getBackgroundColorForStyle(options));

            // always assume disabled==false here since most controls don't support ForeColor on disabled states
            // and have to be set separately.
            // some specific controls provide more specific setters (like StylableTextBox with HintForeColor and TextForeColor).
            // to not override the color everytime, we ignore non-browsable setters
            setIfBrowsable(control, "ForeColor", () => control.ForeColor = getForegroundColorForStyle(options, false));

            Type t = control.GetType();
            ThemePlugins.TryGetValue(t, out IThemePlugin? plugin);
            if (plugin is not null)
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
                applyTreeView(tv);
            }

            if (control is StylableButton sb)
            {
                sb.EnabledBackColor = ButtonBackColor;
                sb.EnabledForeColor = ButtonForeColor;
                sb.EnabledHoverColor = ButtonHoverColor;
                sb.BorderColor = ControlBorderColor;

                sb.DisabledBackColor = ButtonHoverColor;
                sb.DisabledForeColor = getForegroundColorForStyle(options, true);
            }

            if (control is StylableDateTimePicker dtp)
            {
                dtp.EnabledBackColor = getBackgroundColorForStyle(options);
                dtp.EnabledForeColor = getForegroundColorForStyle(options, false);
                dtp.DisabledBackColor = getBackgroundColorForStyle(options);
                dtp.DisabledForeColor = getForegroundColorForStyle(options, true);
            }
            if (control is DataGridView dgv)
            {
                applyDataGridView(dgv);
            }

            if (control is System.Windows.Forms.ToolStrip ts)
            {
                ts.Renderer = new ThemedToolStripRenderer(
                    new ThemedColorTable(
                        Color.Transparent, ControlBorderColor, ButtonHoverColor, ControlHighlightColor, ControlBackColor),
                    ButtonForeColor,
                    getForegroundColorForStyle(options, true))
                {
                    RoundedEdges = false
                };
            }

            if (control is StylableTextBox stb)
            {
                stb.HintForeColor = getForegroundColorForStyle(ThemeOptions.Hint, stb.Enabled);
                stb.TextForeColor = getForegroundColorForStyle(options, false);
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
                stl.DisabledForeColor = getForegroundColorForStyle(options, true);
            }

            if (control is StylableGroupBox sgb)
            {
                sgb.BorderColor = ControlBorderColor;
                sgb.EnabledForeColor = getForegroundColorForStyle(options, false);
                sgb.DisabledForeColor = getForegroundColorForStyle(options, true);
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
                scb.DisabledForeColor = getForegroundColorForStyle(options, true);
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
                Apply(child, options);
            }
        }

        public void UseLogger(ILoggerFactory loggerFactory)
        {
            Logger = new Logger<ITheme>(loggerFactory);
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

        private void applyDataGridView(DataGridView dgv)
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

        private void applyTreeNode(TreeNode tn)
        {
            tn.BackColor = getBackgroundColorForStyle(ThemeOptions.None);
            tn.ForeColor = getForegroundColorForStyle(ThemeOptions.None, false);
            foreach (TreeNode child in tn.Nodes)
            {
                applyTreeNode(child);
            }
        }

        private void applyTreeView(TreeView tv)
        {
            foreach (TreeNode child in tv.Nodes)
            {
                applyTreeNode(child);
            }
        }

        private Color getBackgroundColorForStyle(ThemeOptions options)
        {
            return options switch
            {
                ThemeOptions.Success => ControlSuccessBackColor,
                ThemeOptions.Warning => ControlWarningBackColor,
                ThemeOptions.Error => ControlErrorBackColor,
                _ => ControlBackColor,
            };
        }

        private Color getForegroundColorForStyle(ThemeOptions options, bool disabled)
        {
            Color baseColor = options switch
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
        /// <summary>
        /// some specific controls provide more specific setters (like StylableTextBox with HintForeColor and TextForeColor).
        /// to not override the color everytime,this method is used to only set the color if the property is browsable (aka not hidden)
        /// </summary>
        /// <param name="control">the control to set the color on</param>
        /// <param name="propName">the name of the property</param>
        /// <param name="setter">the setter to be executed if the property is browsable</param>
        private static void setIfBrowsable(Control control, string propName, Action setter)
        {
            BrowsableAttribute? attr = control.GetType().GetProperty(propName)!.GetCustomAttribute<BrowsableAttribute>();
            if (attr?.Browsable != false)
            {
                setter();
            }
        }
    }
}
