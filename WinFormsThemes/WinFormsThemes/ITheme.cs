using Microsoft.Extensions.Logging;
using WinFormsThemes.Themes;

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
        IList<string> AdvancedCapabilities { get; }

        /// <summary>
        /// the capabilities of this theme
        /// </summary>
        ThemeCapabilities Capabilities { get; }

        /// <summary>
        /// the name of the theme
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets a dictionary of plugins which support styling of custom controls without reimplementing the whole theme
        /// </summary>
        IDictionary<Type, IThemePlugin> ThemePlugins { get; set; }

        /// <summary>
        /// apply this theme to the given form
        /// </summary>
        /// <param name="form"></param>
        void Apply(Form form);

        /// <summary>
        /// apply this theme to the given form
        /// </summary>
        void Apply(Form form, ThemeOptions options);

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

        /// <summary>
        /// create a logger from the given factory and use that for logging
        /// </summary>
        /// <param name="loggerFactory">the logging factory to use</param>
        void UseLogger(ILoggerFactory loggerFactory) { }
    }
}
