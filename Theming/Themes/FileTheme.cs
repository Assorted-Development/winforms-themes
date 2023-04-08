﻿using MFBot_1701_E.Utilities;
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
        protected override Color BackgroundColor { get; }
        protected override Color ForegroundColor { get; }

        protected override Color ControlBackColor { get; }
        protected override Color ControlForeColor { get; }
        protected override Color ControlHighlightColor { get; }

        protected override Color ButtonBackColor { get; }
        protected override Color ButtonForeColor { get; }
        protected override Color ButtonHoverColor { get; }

        protected override Color ControlSuccessBackColor { get; }
        protected override Color ControlSuccessForeColor { get; }
        protected override Color ControlWarningBackColor { get; }
        protected override Color ControlWarningForeColor { get; }
        protected override Color ControlErrorBackColor { get; }
        protected override Color ControlErrorForeColor { get; }

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
