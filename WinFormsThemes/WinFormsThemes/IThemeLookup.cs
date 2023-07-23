namespace WinFormsThemes
{
    /// <summary>
    /// interface to detect installed themes
    /// </summary>
    internal interface IThemeLookup
    {
        /// <summary>
        /// the order if more than 1 lookup exists. The lookup with the highest order overrides other themes with the same name
        /// </summary>
        int Order { get; }
        /// <summary>
        /// find all existing themes
        /// </summary>
        /// <returns></returns>
        List<ITheme> Lookup();
    }
}
