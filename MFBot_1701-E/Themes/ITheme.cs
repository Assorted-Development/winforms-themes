using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFBot_1701_E.Themes
{
    /// <summary>
    /// Base class for all themes
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// the name of the theme
        /// </summary>
        string Name { get; }
        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        ThemeCapabilities Capabilities { get; }
        /// <summary>
        /// apply this theme to the given form
        /// </summary>
        /// <param name="form"></param>
        void Apply(Form form);
        /// <summary>
        /// apply this theme to the given control
        /// </summary>
        /// <param name="control"></param>
        void Apply(Control control);
        /// <summary>
        /// apply this theme to the given control
        /// </summary>
        /// <param name="control"></param>
        void Apply(Control control, ThemeOptions options);
    }
}
