using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFBot_1701_E.Themes
{
    /// <summary>
    /// Special options for styling specific controls
    /// </summary>
    public enum ThemeOptions
    {
        /// <summary>
        /// No special options
        /// </summary>
        None = 0,
        /// <summary>
        /// Style control as a success message
        /// </summary>
        Success = 1,
        /// <summary>
        /// Style control as a warning
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Style control as an error
        /// </summary>
        Error = 3
    }
}
