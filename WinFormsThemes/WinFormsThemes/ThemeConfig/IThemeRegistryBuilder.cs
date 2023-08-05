using Microsoft.Extensions.Logging;
using WinFormsThemes.Themes;

namespace WinFormsThemes
{
    public interface IThemeRegistryBuilder
    {
        /// <summary>
        /// Add a plugin to handle additional controls for Themes that support it
        /// </summary>
        /// <typeparam name="T">the Control to handle</typeparam>
        /// <param name="plugin">the plugin handling the theming</param>
        IThemeRegistryBuilder AddThemePlugin<T>(IThemePlugin plugin) where T : Control;

        /// <summary>
        /// return the final IThemeRegistry
        /// </summary>
        /// <returns></returns>
        IThemeRegistry Build();

        /// <summary>
        /// allows specifying the list of themes. If not specified, the default list will be used
        /// </summary>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder WithThemes();
    }

    /// <summary>
    /// allows specifying the list of themes. If not specified, the default list will be used
    /// </summary>
    public interface IThemeRegistryThemeListBuilder
    {
        /// <summary>
        /// Adds DefaultLightTheme, DefaultDarkTheme, DefaultHighContrastTheme
        /// </summary>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder AddDefaultThemes();

        /// <summary>
        /// Adds a custom theme
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder AddTheme(ITheme theme);

        /// <summary>
        /// this completes the theme list
        /// </summary>
        /// <returns></returns>
        IThemeRegistryBuilder FinishThemeList();

        /// <summary>
        /// Adds all themes from FileSystemThemeLookup and ResourceThemeLookup
        /// </summary>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder FromDefaultLookups();

        /// <summary>
        /// adds FileSystemThemeLookup to the list of lookups
        /// </summary>
        /// <param name="themeFolder">the folder to search for the themes. If not set, the default will be used</param>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder WithFileLookup(DirectoryInfo? themeFolder = null);

        /// <summary>
        /// Adds custom themes from the given source
        /// </summary>
        /// <param name="themeLookup"></param>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder WithLookup(IThemeLookup themeLookup);

        /// <summary>
        /// adds ResourceThemeLookup to the list of lookups
        /// </summary>
        /// <param name="resourcePrefix">the prefix to detect the themes in the resources. If not set, the default will be used</param>
        /// <returns></returns>
        IThemeRegistryThemeListBuilder WithResourceLookup(string? resourcePrefix = null);
    }
}