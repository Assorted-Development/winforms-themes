namespace WinFormsThemes
{
    /// <summary>
    /// Base class for all themes
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// This allows custom themes to add additional tags and capabilities to support more specific theme filtering
        /// </summary>
        IList<String> AdvancedCapabilities { get; }

        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        ThemeCapabilities Capabilities { get; }

        /// <summary>
        /// the name of the theme
        /// </summary>
        string Name { get; }
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