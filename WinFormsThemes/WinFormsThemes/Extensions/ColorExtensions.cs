using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject")]

namespace WinFormsThemes.Extensions
{
    internal static class ColorExtensions
    {
        /// <summary>
        /// return the Color from the hex color value
        /// </summary>
        /// <param name="hexColor"></param>
        public static Color ToColor(this string? hexColor)
        {
            if (hexColor is null)
            {
                return SystemColors.Control;
            }
            return ColorTranslator.FromHtml(hexColor);
        }
    }
}