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
    /// Implementation of a light theme. Depends on the default colors of forms
    /// </summary>
    //TODO: Currently, this does not switch back from dark mode as we depend on the colors of the forms
    //and just use this theme to colorize warnings and so on
    public class DefaultLightTheme : ITheme
    {
        public const string THEME_NAME = "LIGHT_DEFAULT";

        public string Name => THEME_NAME;
        public ThemeCapabilities Capabilities => ThemeCapabilities.LightMode;

        public void Apply(Form form) { }

        public void Apply(Control control) { }

        public void Apply(Control control, ThemeOptions options)
        {
            switch(options)
            {
                case ThemeOptions.Success:
                    control.BackColor = Color.Green;
                    break;
                case ThemeOptions.Warning:
                    control.BackColor = Color.Yellow;
                    break;
                case ThemeOptions.Error:
                    control.BackColor = Color.Red;
                    break;
            }
        }
    }
}
