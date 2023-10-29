using System.Diagnostics.CodeAnalysis;

namespace WinFormsThemes
{
    /// <summary>
    /// Holder of the central theme registry for applications without dependency injection and only a single theme registry
    /// </summary>
    public static class ThemeRegistryHolder
    {
        /// <summary>
        /// returns a builder to create a new IThemeRegistry
        /// </summary>
        [SuppressMessage("Design", "CA1024:Use properties where appropriate")]
        public static IThemeRegistryBuilder GetBuilder() => new ThemeRegistryBuilder();

        /// <summary>
        /// the instance of <see cref="IThemeRegistry"/> the <see cref="ThemeRegistryHolder"/> currently holds.
        /// </summary>
        public static IThemeRegistry? ThemeRegistry { get; set; }
    }
}
