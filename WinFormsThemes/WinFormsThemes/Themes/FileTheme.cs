using System.Text.Json;
using System.Text.Json.Nodes;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// a generic theme that loads its config from a file
    /// </summary>
    internal class FileTheme : AbstractTheme
    {
        public override string Name { get; }
        public override ThemeCapabilities Capabilities { get; }
        public override IList<string> AdvancedCapabilities { get; }
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

        public override Color TableBackColor { get; }
        public override Color TableHeaderBackColor { get; }
        public override Color TableHeaderForeColor { get; }
        public override Color TableSelectionBackColor { get; }
        public override Color TableCellBackColor { get; }
        public override Color TableCellForeColor { get; }

        public override Color ListViewHeaderGroupColor { get; }

        public override Color ComboBoxItemBackColor { get; }
        public override Color ComboBoxItemHoverColor { get; }

        public override Color ControlHighlightLightColor { get; }
        public override Color ControlHighlightDarkColor { get; }
        public override Color ControlBorderColor { get; }
        public override Color ControlBorderLightColor { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="doc">the doc to load from</param>
        private FileTheme(JsonNode doc)
        {
            Name = (string)doc["name"];
            JsonArray caps = (JsonArray)doc["capabilities"];
            List<String> advancedCaps = new List<string>();
            foreach (string s in caps)
            {
                ThemeCapabilities tmp;
                if (Enum.IsDefined(typeof(ThemeCapabilities), s))
                {
                    Capabilities |= Enum.Parse<ThemeCapabilities>(s);
                }
                else
                {
                    advancedCaps.Add(s);
                }
            }
            AdvancedCapabilities = advancedCaps.AsReadOnly();

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

                ButtonBackColor = ((string)doc["colors"]["buttonBackColor"]).ToColor();
                ButtonForeColor = ((string)doc["colors"]["buttonForeColor"]).ToColor();
                ButtonHoverColor = ((string)doc["colors"]["buttonHoverColor"]).ToColor();

                ControlSuccessBackColor = ((string)doc["colors"]["successBackColor"]).ToColor();
                ControlSuccessForeColor = ((string)doc["colors"]["successForeColor"]).ToColor();
                ControlWarningBackColor = ((string)doc["colors"]["warningBackColor"]).ToColor();
                ControlWarningForeColor = ((string)doc["colors"]["warningForeColor"]).ToColor();
                ControlErrorBackColor = ((string)doc["colors"]["errorBackColor"]).ToColor();
                ControlErrorForeColor = ((string)doc["colors"]["errorForeColor"]).ToColor();

                //backwards compatibility for themes V2
                TableBackColor = ControlBackColor;
                TableHeaderBackColor = TableBackColor;
                TableHeaderForeColor = ControlForeColor;
                TableSelectionBackColor = ControlHighlightColor;
                TableCellBackColor = TableBackColor;
                TableCellForeColor = ControlForeColor;
                ListViewHeaderGroupColor = GetSoftenedColor(ControlHighlightColor, true);
                ComboBoxItemBackColor = ControlHighlightColor;
                ComboBoxItemHoverColor = GetSoftenedColor(ControlHighlightColor, true);
                ControlHighlightLightColor = GetSoftenedColor(ControlBorderColor, true);
                ControlHighlightDarkColor = GetSoftenedColor(ControlBorderColor);
                ControlBorderColor = ControlHighlightColor;
                ControlBorderLightColor = ControlHighlightColor;
            }
            if(themeVersion >= 2)
            {
                TableBackColor = ((string)doc["colors"]["tableBackColor"]).ToColor();
                TableHeaderBackColor = ((string)doc["colors"]["tableHeaderBackColor"]).ToColor();
                TableHeaderForeColor = ((string)doc["colors"]["tableHeaderForeColor"]).ToColor();
                TableSelectionBackColor = ((string)doc["colors"]["tableSelectionBackColor"]).ToColor();
                TableCellBackColor = ((string)doc["colors"]["tableCellBackColor"]).ToColor();
                TableCellForeColor = ((string)doc["colors"]["tableCellForeColor"]).ToColor();
                ListViewHeaderGroupColor = ((string)doc["colors"]["listViewHeaderGroupColor"]).ToColor();
                ComboBoxItemBackColor = ((string)doc["colors"]["comboBoxItemBackColor"]).ToColor();
                ComboBoxItemHoverColor = ((string)doc["colors"]["comboBoxItemHoverColor"]).ToColor();
                ControlHighlightLightColor = ((string)doc["colors"]["controlHighlightLightColor"]).ToColor();
                ControlHighlightDarkColor = ((string)doc["colors"]["controlHighlightDarkColor"]).ToColor();
                ControlBorderColor = ((string)doc["colors"]["controlBorderColor"]).ToColor();
                ControlBorderLightColor = ((string)doc["colors"]["controlBorderLightColor"]).ToColor();
            }
        }

        /// <summary>
        /// Parse a theme JSON config
        /// </summary>
        /// <param name="jsonContent">the JSON content</param>
        /// <returns></returns>
        public static FileTheme? Load(string jsonContent)
        {
            try
            {
                return new FileTheme(JsonNode.Parse(jsonContent));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
