using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Themes
{
    /// <summary>
    /// registry for all Themes
    /// </summary>
    public static class ThemeRegistry
    {
        /// <summary>
        /// List of all Themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> THEMES = new Dictionary<string, ITheme>()
        {
            { DefaultDarkTheme.THEME_NAME, new DefaultDarkTheme() },
            { HighContrastDarkTheme.THEME_NAME, new HighContrastDarkTheme() }
        };
        /// <summary>
        /// retuns the list of all theme names
        /// </summary>
        /// <returns></returns>
        public static List<String> ListNames()
        {
            return THEMES.Keys.ToList();
        }
        /// <summary>
        /// retuns all themes
        /// </summary>
        /// <returns></returns>
        public static List<ITheme> List()
        {
            return THEMES.Values.ToList();
        }
        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITheme Get(string name)
        {
            return THEMES.ContainsKey(name) ? THEMES[name] : null;
        }
        /// <summary>
        /// return the theme with the matching capabilities
        /// </summary>
        /// <param name="caps"></param>
        /// <returns></returns>
        public static ITheme Get(ThemeCapabilities caps)
        {
            return List().Where(t => (t.Capabilities & caps) == caps).FirstOrDefault();
        }
    }
}
