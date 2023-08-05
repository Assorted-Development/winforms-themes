using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

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
        /// <param name="loggerFactory">the loggerFactory to use for this library</param>
        public static IThemeRegistryBuilder GetBuilder(ILoggerFactory loggerFactory) => new ThemeRegistryBuilder(loggerFactory);
        /// <summary>
        /// returns a builder to create a new IThemeRegistry
        /// </summary>
        public static IThemeRegistryBuilder GetBuilder() => new ThemeRegistryBuilder(new NullLoggerFactory());

        /// <summary>
        /// the instance of <see cref="IThemeRegistry"/> the <see cref="ThemeRegistryHolder"/> currently holds.
        /// </summary>
        public static IThemeRegistry? ThemeRegistry { get; set; }
    }
}