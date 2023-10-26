using Microsoft.Extensions.Logging;

namespace WinFormsThemes
{
    /// <summary>
    /// interface to detect installed themes
    /// </summary>
    public interface IThemeLookup
    {
        /// <summary>
        /// the order if more than 1 lookup exists. The lookup with the highest order overrides other themes with the same name
        /// </summary>
        int Order { get; }

        /// <summary>
        /// find all existing themes
        /// </summary>
        IList<ITheme> Lookup();

        /// <summary>
        /// create a logger from the given factory and use that for logging
        /// </summary>
        /// <param name="loggerFactory">the logging factory to use</param>
        void UseLogger(ILoggerFactory loggerFactory) { }
    }
}