using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// a generic theme that loads its config from a file
    /// </summary>
    internal sealed class FileTheme : AbstractTheme
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="doc">the doc to load from</param>
        private FileTheme(JsonNode doc)
        {
            string? name = (string?)doc["name"];
            //require Name to be not null
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Theme name is mandatory");
            }

            Name = name;
            JsonArray? caps = (JsonArray?)doc["capabilities"];

            //require Name to be not null
            if (caps is null || caps.Count == 0)
            {
                throw new ArgumentException("at least one capability must be set");
            }

            Capabilities = getThemeCapabilities(caps);
            AdvancedCapabilities = getAdvancedCapabilities(caps).AsReadOnly();

            //use the theme version to update the configured colors if necessary
            //e.g. when a new version adds a new color you may calculate the missing value from the existing ones
            int themeVersion = ((int?)doc["version"]) ?? 1;
            if (themeVersion >= 1)
            {
                BackgroundColor = ((string?)doc["colors"]?["backColor"]).ToColor();
                ForegroundColor = ((string?)doc["colors"]?["foreColor"]).ToColor();

                ControlBackColor = ((string?)doc["colors"]?["controlBackColor"]).ToColor();
                ControlForeColor = ((string?)doc["colors"]?["controlForeColor"]).ToColor();
                ControlHighlightColor = ((string?)doc["colors"]?["controlHighlightColor"]).ToColor();

                ButtonBackColor = ((string?)doc["colors"]?["buttonBackColor"]).ToColor();
                ButtonForeColor = ((string?)doc["colors"]?["buttonForeColor"]).ToColor();
                ButtonHoverColor = ((string?)doc["colors"]?["buttonHoverColor"]).ToColor();

                ControlSuccessBackColor = ((string?)doc["colors"]?["successBackColor"]).ToColor();
                ControlSuccessForeColor = ((string?)doc["colors"]?["successForeColor"]).ToColor();
                ControlWarningBackColor = ((string?)doc["colors"]?["warningBackColor"]).ToColor();
                ControlWarningForeColor = ((string?)doc["colors"]?["warningForeColor"]).ToColor();
                ControlErrorBackColor = ((string?)doc["colors"]?["errorBackColor"]).ToColor();
                ControlErrorForeColor = ((string?)doc["colors"]?["errorForeColor"]).ToColor();

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
            if (themeVersion >= 2)

            {
                TableBackColor = ((string?)doc["colors"]?["tableBackColor"]).ToColor();
                TableHeaderBackColor = ((string?)doc["colors"]?["tableHeaderBackColor"]).ToColor();
                TableHeaderForeColor = ((string?)doc["colors"]?["tableHeaderForeColor"]).ToColor();
                TableSelectionBackColor = ((string?)doc["colors"]?["tableSelectionBackColor"]).ToColor();
                TableCellBackColor = ((string?)doc["colors"]?["tableCellBackColor"]).ToColor();
                TableCellForeColor = ((string?)doc["colors"]?["tableCellForeColor"]).ToColor();
                ListViewHeaderGroupColor = ((string?)doc["colors"]?["listViewHeaderGroupColor"]).ToColor();
                ComboBoxItemBackColor = ((string?)doc["colors"]?["comboBoxItemBackColor"]).ToColor();
                ComboBoxItemHoverColor = ((string?)doc["colors"]?["comboBoxItemHoverColor"]).ToColor();
                ControlHighlightLightColor = ((string?)doc["colors"]?["controlHighlightLightColor"]).ToColor();
                ControlHighlightDarkColor = ((string?)doc["colors"]?["controlHighlightDarkColor"]).ToColor();
                ControlBorderColor = ((string?)doc["colors"]?["controlBorderColor"]).ToColor();
                ControlBorderLightColor = ((string?)doc["colors"]?["controlBorderLightColor"]).ToColor();
            }
        }

        private static List<string> getAdvancedCapabilities(JsonArray caps)
        {
            List<string> advancedCaps = new();
            foreach (string? s in caps.Select(node => (string?)node))
            {
                if (s is null)
                {
                    continue;
                }

                if (!Enum.IsDefined(typeof(ThemeCapabilities), s))
                {
                    advancedCaps.Add(s);
                }
            }

            return advancedCaps;
        }

        private static ThemeCapabilities getThemeCapabilities(JsonArray caps)
        {
            ThemeCapabilities capabilities = ThemeCapabilities.None;
            foreach (string? s in caps.Select(node => (string?)node))
            {
                if (s is null)
                {
                    continue;
                }

                if (Enum.IsDefined(typeof(ThemeCapabilities), s))
                {
                    capabilities |= Enum.Parse<ThemeCapabilities>(s);
                }
            }

            return capabilities;
        }

        [ExcludeFromCodeCoverage]
        public override IList<string> AdvancedCapabilities { get; }

        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor { get; }

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities { get; }

        [ExcludeFromCodeCoverage]
        public override Color ComboBoxItemBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ComboBoxItemHoverColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlBorderColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlBorderLightColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlErrorForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightDarkColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightLightColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ControlWarningForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ForegroundColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color ListViewHeaderGroupColor { get; }

        [ExcludeFromCodeCoverage]
        public override string Name { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableCellBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableCellForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableHeaderBackColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableHeaderForeColor { get; }

        [ExcludeFromCodeCoverage]
        public override Color TableSelectionBackColor { get; }

        /// <summary>
        /// Parse a theme JSON config
        /// </summary>
        /// <param name="jsonContent">the JSON content</param>
        public static FileTheme? Load(string jsonContent)
        {
            try
            {
                JsonNode? json = JsonNode.Parse(jsonContent);
                if (json is null)
                {
                    return null;
                }

                return new FileTheme(json);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
