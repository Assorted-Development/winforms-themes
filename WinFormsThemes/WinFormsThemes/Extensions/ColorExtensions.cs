namespace WinFormsThemes.Extensions
{
    internal static class ColorExtensions
    {
        /// <summary>
        /// return the Color from the hex color value
        /// </summary>
        /// <param name="hexColor"></param>
        /// <returns></returns>
        public static Color ToColor(this string? hexColor)
        {
            if (hexColor == null)
            {
                return SystemColors.Control;
            }
            return ColorTranslator.FromHtml(hexColor);
        }
    }
}