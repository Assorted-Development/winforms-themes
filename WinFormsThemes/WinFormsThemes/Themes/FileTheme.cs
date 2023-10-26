using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Json.Schema;
using Microsoft.Extensions.Logging;
using WinFormsThemes.Extensions;

namespace WinFormsThemes.Themes
{
    /// <summary>
    /// a generic theme that loads its config from a file
    /// </summary>
    internal sealed class FileTheme : AbstractTheme
    {
        private static readonly Regex HEX_COLOR_VALUE = new("^#[0-9A-Fa-f]{6}$", RegexOptions.Compiled);
        [ExcludeFromCodeCoverage]
        public override IList<string> AdvancedCapabilities { get; }

        private Color _backgroundColor;
        [ExcludeFromCodeCoverage]
        public override Color BackgroundColor => _backgroundColor;

        private Color _buttonBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ButtonBackColor => _buttonBackColor;

        private Color _buttonForeColor;
        [ExcludeFromCodeCoverage]
        public override Color ButtonForeColor => _buttonForeColor;

        private Color _buttonHoverColor;
        [ExcludeFromCodeCoverage]
        public override Color ButtonHoverColor => _buttonHoverColor;

        [ExcludeFromCodeCoverage]
        public override ThemeCapabilities Capabilities { get; }

        private Color _comboBoxItemBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ComboBoxItemBackColor => _comboBoxItemBackColor;

        private Color _comboBoxItemHoverColor;
        [ExcludeFromCodeCoverage]
        public override Color ComboBoxItemHoverColor => _comboBoxItemHoverColor;

        private Color _controlBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlBackColor => _controlBackColor;

        private Color _controlBorderColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlBorderColor => _controlBorderColor;

        private Color _controlErrorBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlErrorBackColor => _controlErrorBackColor;

        private Color _controlErrorForeColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlErrorForeColor => _controlErrorForeColor;

        private Color _controlForeColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlForeColor => _controlForeColor;

        private Color _controlHighlightColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlHighlightColor => _controlHighlightColor;

        private Color _controlSuccessBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessBackColor => _controlSuccessBackColor;

        private Color _controlSuccessForeColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlSuccessForeColor => _controlSuccessForeColor;

        private Color _controlWarningBackColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlWarningBackColor => _controlWarningBackColor;

        private Color _controlWarningForeColor;
        [ExcludeFromCodeCoverage]
        public override Color ControlWarningForeColor => _controlWarningForeColor;

        private Color _foregroundColor;
        [ExcludeFromCodeCoverage]
        public override Color ForegroundColor => _foregroundColor;

        private Color _listViewHeaderGroupColor;
        [ExcludeFromCodeCoverage]
        public override Color ListViewHeaderGroupColor => _listViewHeaderGroupColor;

        [ExcludeFromCodeCoverage]
        public override string Name { get; }

        private Color _tableBackColor;
        [ExcludeFromCodeCoverage]
        public override Color TableBackColor => _tableBackColor;

        private Color _tableCellBackColor;
        [ExcludeFromCodeCoverage]
        public override Color TableCellBackColor => _tableCellBackColor;

        private Color _tableCellForeColor;
        [ExcludeFromCodeCoverage]
        public override Color TableCellForeColor => _tableCellForeColor;

        private Color _tableHeaderBackColor;
        [ExcludeFromCodeCoverage]
        public override Color TableHeaderBackColor => _tableHeaderBackColor;

        private Color _tableHeaderForeColor;
        [ExcludeFromCodeCoverage]
        public override Color TableHeaderForeColor => _tableHeaderForeColor;

        private Color _tableSelectionBackColor;
        [ExcludeFromCodeCoverage]
        public override Color TableSelectionBackColor => _tableSelectionBackColor;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="doc">the doc to load from</param>
        private FileTheme(JsonNode doc)
        {
            string? name = (string?)doc["name"];
            JsonArray? caps = (JsonArray?)doc["capabilities"];

            validateThemeProperties(name, caps);

            Name = name;
            Capabilities = getThemeCapabilities(caps);
            AdvancedCapabilities = getAdvancedCapabilities(caps).AsReadOnly();

            //use the theme version to update the configured colors if necessary
            //e.g. when a new version adds a new color you may calculate the missing value from the existing ones
            int themeVersion = ((int?)doc["version"]) ?? 1;
            if (themeVersion >= 3)
            {
                loadFromNewFileStructure(doc);
            }
            else
            {
                loadFromOldFileStructure(doc);
            }
        }
        /// <summary>
        /// run the old import logic. can be removed after all json files in this repo are migrated
        /// </summary>
        /// <param name="doc"></param>
        private void loadFromOldFileStructure(JsonNode doc)
        {
            int themeVersion = ((int?)doc["version"]) ?? 1;
            if (themeVersion >= 1)
            {
                _backgroundColor = ((string?)doc["colors"]?["backColor"]).ToColor();
                _foregroundColor = ((string?)doc["colors"]?["foreColor"]).ToColor();

                _controlBackColor = ((string?)doc["colors"]?["controlBackColor"]).ToColor();
                _controlForeColor = ((string?)doc["colors"]?["controlForeColor"]).ToColor();
                _controlHighlightColor = ((string?)doc["colors"]?["controlHighlightColor"]).ToColor();

                _buttonBackColor = ((string?)doc["colors"]?["buttonBackColor"]).ToColor();
                _buttonForeColor = ((string?)doc["colors"]?["buttonForeColor"]).ToColor();
                _buttonHoverColor = ((string?)doc["colors"]?["buttonHoverColor"]).ToColor();

                _controlSuccessBackColor = ((string?)doc["colors"]?["successBackColor"]).ToColor();
                _controlSuccessForeColor = ((string?)doc["colors"]?["successForeColor"]).ToColor();
                _controlWarningBackColor = ((string?)doc["colors"]?["warningBackColor"]).ToColor();
                _controlWarningForeColor = ((string?)doc["colors"]?["warningForeColor"]).ToColor();
                _controlErrorBackColor = ((string?)doc["colors"]?["errorBackColor"]).ToColor();
                _controlErrorForeColor = ((string?)doc["colors"]?["errorForeColor"]).ToColor();

                //backwards compatibility for themes V2
                _tableBackColor = ControlBackColor;
                _tableHeaderBackColor = TableBackColor;
                _tableHeaderForeColor = ControlForeColor;
                _tableSelectionBackColor = ControlHighlightColor;
                _tableCellBackColor = TableBackColor;
                _tableCellForeColor = ControlForeColor;
                _listViewHeaderGroupColor = GetSoftenedColor(ControlHighlightColor, true);
                _comboBoxItemBackColor = ControlHighlightColor;
                _comboBoxItemHoverColor = GetSoftenedColor(ControlHighlightColor, true);
                _controlBorderColor = ControlHighlightColor;
            }

            if (themeVersion >= 2)
            {
                _tableBackColor = ((string?)doc["colors"]?["tableBackColor"]).ToColor();
                _tableHeaderBackColor = ((string?)doc["colors"]?["tableHeaderBackColor"]).ToColor();
                _tableHeaderForeColor = ((string?)doc["colors"]?["tableHeaderForeColor"]).ToColor();
                _tableSelectionBackColor = ((string?)doc["colors"]?["tableSelectionBackColor"]).ToColor();
                _tableCellBackColor = ((string?)doc["colors"]?["tableCellBackColor"]).ToColor();
                _tableCellForeColor = ((string?)doc["colors"]?["tableCellForeColor"]).ToColor();
                _listViewHeaderGroupColor = ((string?)doc["colors"]?["listViewHeaderGroupColor"]).ToColor();
                _comboBoxItemBackColor = ((string?)doc["colors"]?["comboBoxItemBackColor"]).ToColor();
                _comboBoxItemHoverColor = ((string?)doc["colors"]?["comboBoxItemHoverColor"]).ToColor();
                _controlBorderColor = ((string?)doc["colors"]?["controlBorderColor"]).ToColor();
            }
        }
        /// <summary>
        /// loads the JSON Schema for validation
        /// </summary>
        private static JsonSchema? getSchema()
        {
            using Stream? str = Assembly.GetExecutingAssembly().GetManifestResourceStream("WinFormsThemes.themes.schema.json");
            if (str is not null)
            {
                return JsonSchema.FromStream(str).AsTask().Result;
            }
            return null;
        }
        /// <summary>
        /// load all color variables
        /// </summary>
        /// <param name="vars"></param>
        private static Dictionary<string, Color> loadVariables(JsonNode? vars)
        {
            Dictionary<string, Color> result = new();
            if (vars is not null)
            {
                foreach (string k in vars.AsObject().Select(p => p.Key).ToList())
                {
                    result.Add(k, ((string?)vars[k]).ToColor());
                }
            }
            return result;
        }
        /// <summary>
        /// parse the given color code
        /// </summary>
        /// <param name="value">the hex color code</param>
        /// <param name="defaultColor">the default color if no hex code is set</param>
        /// <param name="vars">the dictionary for looking up variable declarations</param>
        /// <exception cref="ArgumentException">the color could not be parsed</exception>
        private static Color parseColor(JsonNode? value, Color defaultColor, Dictionary<string, Color> vars)
        {
            string? hexColor = (string?)value;
            if (hexColor is null)
            {
                return defaultColor;
            }
            if (vars.ContainsKey(hexColor))
            {
                return vars[hexColor];
            }
            if (HEX_COLOR_VALUE.IsMatch(hexColor))
            {
                return hexColor.ToColor();
            }
            else
            {
                throw new ArgumentException($"Invalid color '{hexColor}' - color is not a valid hex value and was not defined as a variable!");
            }
        }
        /// <summary>
        /// load the new JSON structure including schema validation
        /// </summary>
        /// <param name="doc">the theme to load</param>
        /// <exception cref="ArgumentException">if the document was not valid</exception>
        private void loadFromNewFileStructure(JsonNode doc)
        {
            if (getSchema()?.Evaluate(doc)?.IsValid == false)
            {
                throw new ArgumentException("Invalid schema");
            }
            Dictionary<string, Color> vars = loadVariables(doc["variables"]);
            JsonNode colors = doc["colors"]!;
            _backgroundColor = parseColor(colors["backColor"], Color.Empty, vars);
            _foregroundColor = parseColor(colors["foreColor"], Color.Empty, vars);

            JsonNode controls = colors["controls"]!;
            _controlBackColor = parseColor(controls["backColor"], Color.Empty, vars);
            _controlForeColor = parseColor(controls["foreColor"], Color.Empty, vars);
            _controlHighlightColor = parseColor(controls["highlightColor"], _controlBackColor, vars);
            _controlBorderColor = parseColor(controls["borderColor"], _controlBackColor, vars);

            JsonNode? button = colors["button"];
            _buttonBackColor = parseColor(button?["backColor"], _controlBackColor, vars);
            _buttonForeColor = parseColor(button?["foreColor"], _controlForeColor, vars);
            _buttonHoverColor = parseColor(button?["hoverColor"], _buttonBackColor, vars);

            JsonNode? comboBox = colors["comboBox"];
            _comboBoxItemBackColor = parseColor(comboBox?["itemBackColor"], _controlBackColor, vars);
            _comboBoxItemHoverColor = parseColor(comboBox?["itemHoverColor"], _controlBackColor, vars);

            JsonNode? listView = colors["listView"];
            _listViewHeaderGroupColor = parseColor(listView?["headerGroupColor"], _controlBackColor, vars);

            JsonNode? dataGridView = colors["dataGridView"];
            _tableBackColor = parseColor(dataGridView?["backColor"], _controlBackColor, vars);
            _tableHeaderBackColor = parseColor(dataGridView?["headerBackColor"], _tableBackColor, vars);
            _tableHeaderForeColor = parseColor(dataGridView?["headerForeColor"], _controlForeColor, vars);
            _tableSelectionBackColor = parseColor(dataGridView?["selectionBackColor"], _tableBackColor, vars);
            _tableCellBackColor = parseColor(dataGridView?["cellBackColor"], _tableBackColor, vars);
            _tableCellForeColor = parseColor(dataGridView?["cellForeColor"], _controlForeColor, vars);

            JsonNode? status = colors["status"];
            _controlSuccessBackColor = parseColor(status?["success"]?["backColor"], _controlBackColor, vars);
            _controlSuccessForeColor = parseColor(status?["success"]?["foreColor"], _controlForeColor, vars);
            _controlWarningBackColor = parseColor(status?["warning"]?["backColor"], _controlBackColor, vars);
            _controlWarningForeColor = parseColor(status?["warning"]?["foreColor"], _controlForeColor, vars);
            _controlErrorBackColor = parseColor(status?["error"]?["backColor"], _controlBackColor, vars);
            _controlErrorForeColor = parseColor(status?["error"]?["foreColor"], _controlForeColor, vars);

        }

        /// <summary>
        /// Parse a theme JSON config
        /// </summary>
        /// <param name="jsonContent">the JSON content</param>
        public static FileTheme? Load(string jsonContent, ILogger log)
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
            catch (Exception ex)
            {
                log.Log(LogLevel.Error, ex, "failed to read theme JSON");
                return null;
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

        private static void validateThemeProperties([NotNull] string? name, [NotNull] JsonArray? caps)
        {
            //require Name to be not null
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Theme name is mandatory");
            }

            //require Name to be not null
            if (caps is null || caps.Count == 0)
            {
                throw new ArgumentException("at least one capability must be set");
            }
        }
    }
}
