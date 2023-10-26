
namespace WinFormsThemes.Themes
{
    /// <summary>
    /// helper implementation to make implementing <see cref="IThemePlugin"/> easier
    /// </summary>
    /// <typeparam name="T">the control type this plugin supports</typeparam>
    public abstract class AbstractThemePlugin<T> : IThemePlugin where T : Control
    {
        public void Apply(Control control, AbstractTheme theme)
        {
            ApplyPlugin((T)control, theme);
        }

        /// <summary>
        /// should be implemented by the plugin to style the given control
        /// </summary>
        /// <param name="control">the control to style</param>
        /// <param name="theme">the theme to be applied</param>
        protected abstract void ApplyPlugin(T control, AbstractTheme theme);

        public Type GetSupportedType()
        {
            return typeof(T);
        }
    }
}
