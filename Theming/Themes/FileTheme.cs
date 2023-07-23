using MFBot_1701_E.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;

namespace MFBot_1701_E.Theming.Themes
{
    /// <summary>
    /// a generic theme that loads its config from a file
    /// </summary>
    internal class FileTheme : AbstractTheme
    {
        public override string Name { get; }
        public override ThemeCapabilities Capabilities { get; }
        public override Color BackgroundColor { get; }
        public override Color ForegroundColor { get; }

        public override Color ControlBackColor { get; }
        public override Color ControlForeColor { get; }
        public override Color ControlHighlightColor { get; }

        public override Color ButtonBackColor { get; }
        public override Color ButtonForeColor { get; }
        public override Color ButtonHoverColor { get; }

        public override Color ControlSuccessBackColor { get; }
        public override Color ControlSuccessForeColor { get; }
        public override Color ControlWarningBackColor { get; }
        public override Color ControlWarningForeColor { get; }
        public override Color ControlErrorBackColor { get; }
        public override Color ControlErrorForeColor { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="doc">the doc to load from</param>
        private FileTheme(JObject doc)
        {
            Name = (string)doc["name"];
            JArray caps = (JArray)doc["capabilities"];
            foreach (string s in caps)
            {
                Capabilities |= Enum.Parse<ThemeCapabilities>(s);
            }

            //use the theme version to update the configured colors if necessary
            //e.g. when a new version adds a new color you may calculate the missing value from the existing ones
            int themeVersion = (int)doc["version"];
            if (themeVersion >= 1)
            {
                BackgroundColor = ((string)doc["colors"]["backColor"]).ToColor();
                ForegroundColor = ((string)doc["colors"]["foreColor"]).ToColor();

                ControlBackColor = ((string)doc["colors"]["controlBackColor"]).ToColor();
                ControlForeColor = ((string)doc["colors"]["controlForeColor"]).ToColor();
                ControlHighlightColor = ((string)doc["colors"]["controlHighlightColor"]).ToColor();

                ButtonBackColor = ((string)doc["colors"]["buttonHoverColor"]).ToColor();
                ButtonForeColor = ((string)doc["colors"]["buttonHoverColor"]).ToColor();
                ButtonHoverColor = ((string)doc["colors"]["buttonHoverColor"]).ToColor();

                ControlSuccessBackColor = ((string)doc["colors"]["successBackColor"]).ToColor();
                ControlWarningBackColor = ((string)doc["colors"]["warningBackColor"]).ToColor();
                ControlErrorBackColor = ((string)doc["colors"]["errorBackColor"]).ToColor();
                ControlSuccessForeColor = ((string)doc["colors"]["successForeColor"]).ToColor();
                ControlWarningForeColor = ((string)doc["colors"]["warningForeColor"]).ToColor();
                ControlErrorForeColor = ((string)doc["colors"]["errorForeColor"]).ToColor();
            }
        }

        /// <summary>
        /// Parse a theme JSON config
        /// </summary>
        /// <param name="jsonContent">the JSON content</param>
        /// <returns></returns>
        public static FileTheme Load(string jsonContent)
        {
            try
            {
                return new FileTheme(JObject.Parse(jsonContent));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
