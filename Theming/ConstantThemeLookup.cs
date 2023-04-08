using MFBot_1701_E.Theming.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Theming
{
    /// <summary>
    /// themes are fix
    /// </summary>
    internal class ConstantThemeLookup : IThemeLookup
    {
        public int Order => 0;

        public List<ITheme> Lookup()
        {
            return new List<ITheme>() { new DefaultLightTheme(), new DefaultDarkTheme(), new HighContrastDarkTheme() };
        }
    }
}
