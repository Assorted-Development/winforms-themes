using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// Interface for all Theme Plugins
    /// </summary>
    public interface IThemePlugin
    {
        /// <summary>
        /// Apply the given theme to the control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="theme"></param>
        void Apply(Control control, AbstractTheme theme);
    }
}
