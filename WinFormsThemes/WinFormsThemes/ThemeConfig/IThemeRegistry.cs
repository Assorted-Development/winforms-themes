﻿namespace WinFormsThemes
{
    /// <summary>
    /// interface for interacting with the theme registry
    /// </summary>
    public interface IThemeRegistry
    {
        /// <summary>
        /// Event that triggers when the <see cref="Current"/> theme has changed
        /// </summary>
        public event EventHandler? OnThemeChanged;

        /// <summary>
        /// the builder to create a new IThemeRegistry
        /// </summary>
        public static IThemeRegistryBuilder BUILDER => new ThemeRegistryBuilder();

        /// <summary>
        /// the current theme
        /// </summary>
        ITheme? Current { get; set; }

        /// <summary>
        /// returns the theme with capabilities configured by the user
        /// </summary>
        /// <returns></returns>
        ITheme? Get();

        /// <summary>
        /// return the theme with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ITheme? Get(string name);

        /// <summary>
        /// returns a theme with the matching capabilities
        /// </summary>
        /// <param name="caps"></param>
        /// <param name="advancedCapabilitiesFilters"></param>
        /// <returns></returns>
        ITheme? Get(ThemeCapabilities caps, params string[] advancedCapabilitiesFilters);

        /// <summary>
        /// returns all themes
        /// </summary>
        /// <returns></returns>
        List<ITheme> List();

        /// <summary>
        /// returns a list of the names of all themes that are registered in this <see cref="IThemeRegistry"/> instance
        /// </summary>
        /// <returns></returns>
        List<string> ListNames();
    }
}