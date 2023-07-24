using WinFormsThemes.Themes;

namespace WinFormsThemes
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