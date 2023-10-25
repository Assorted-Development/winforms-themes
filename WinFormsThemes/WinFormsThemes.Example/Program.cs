using StylableWinFormsControls.Example;

namespace WinFormsThemes.Example
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ThemeRegistryHolder.ThemeRegistry = ThemeRegistryHolder.GetBuilder().WithCurrentThemeSelector((selector) => ThemeRegistryHolder.ThemeRegistry.Get()).Build();

            Application.Run(new FrmDefault());
        }
    }
}
