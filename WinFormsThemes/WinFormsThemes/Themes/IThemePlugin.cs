namespace WinFormsThemes.Themes
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
        /// <summary>
        /// Returns the Control type that this plugin supports
        /// </summary>
        Type GetSupportedType();
    }
}
