# winforms-themes

[![Nuget](https://img.shields.io/nuget/v/AssortedDevelopment.WinFormsThemes)](https://www.nuget.org/packages/AssortedDevelopment.WinFormsThemes)
[![Nuget](https://img.shields.io/nuget/dt/AssortedDevelopment.WinFormsThemes)](https://www.nuget.org/packages/AssortedDevelopment.WinFormsThemes)
[![Build Status](https://github.com/Assorted-Development/winforms-themes/actions/workflows/prerelease.yml/badge.svg)](https://github.com/Assorted-Development/winforms-themes/actions/workflows/prerelease.yml)

This project adds support for themes in .NET WinForms applications. This project supports both out-of-the-box and custom themes and uses our [winforms-stylable-controls](https://github.com/Assorted-Development/winforms-stylable-controls) project to style controls which are lacking style support.

# License
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

# ToC
* [WinForms Themes](#winforms-themes)
  * [License](#license)
  * [Usage](#usage)
  * [Extended Usage](#extended-usage)
  * [Contributions](#contributions)

## Usage
To use this project, you need to add a reference to our nuget package (`dotnet add package AssortedDevelopment.WinFormsThemes`) first. The easiest way to use our themes is to add a single line in the `OnLoad` in all forms to be themed:
```csharp
ThemeRegistry.Current.Apply(this);
```

This line of code will:
1. check the operating system for the user settings on dark mode and high contrast
2. Load all available themes
3. Look for a theme matching the settings for dark mode and high contrast
4. use the first theme found as your current theme

Last but not least, this will also apply this theme on the given Form and all children.

## Extended Usage
Of course, you can extend this library and customize the handling to fit your needs. Here are a few examples:

### Customize theme selection
By default, our library will honor the settings of the operating system in regard to dark mode and high contrast. If you want to add additional selection criteria or you want to give the user an option to override this selection you can do that easily.
Instead of relying on the default settings in `ThemeRegistry.Get()` which is implicitly called by `ThemeRegistry.Current` when no current theme was set you can set `ThemeRegistry.Current` to any theme you want:
```csharp
List<ITheme> allThemes = ThemeRegistry.List();
ITheme selectedTheme = null;
//logic to select theme here
ThemeRegistry.Current = selectedTheme;
```

### Add custom themes
Out of the box, there are 2 ways you can add custom themes:
- Files with the file ending `.theme.json` stored in a `themes` directory of the working dir.
- Assembly resources in any assembly where the name starts with `CONFIG_THEMING_THEME_`

Both ways use the same JSON format for the theme definition(the version defines the format of the file):
```json
{
	"name": "theme-name",
	"capabilities": ["DarkMode", "LightMode", "HighContrast"],
	"version": 2,
	"colors": {
		"backColor": "#082a56",
		"foreColor": "#082a56",
		"buttonBackColor": "#082a56",
		"buttonForeColor": "#082a56",
		"buttonHoverColor": "#082a56",
		"comboBoxItemBackColor": "#082a56",
		"comboBoxItemHoverColor": "#082a56",
		"controlBackColor": "#082a56",
		"controlForeColor": "#082a56",
		"controlHighlightColor": "#082a56",
		"controlHighlightLightColor": "#082a56",
		"controlHighlightDarkColor": "#082a56",
		"controlBorderColor": "#082a56",
		"controlBorderLightColor": "#082a56",
		"listViewHeaderGroupColor": "#082a56",
		"tableBackColor": "#082a56",
		"tableHeaderBackColor": "#082a56",
		"tableHeaderForeColor": "#082a56",
		"tableSelectionBackColor": "#082a56",
		"tableCellBackColor": "#082a56",
		"tableCellForeColor": "#082a56",
		"successBackColor": "#082a56",
		"successForeColor": "#082a56",
		"warningBackColor": "#082a56",
		"warningForeColor": "#082a56",
		"errorBackColor": "#082a56",
		"errorForeColor": "#082a56"
	}
}
```

If those 2 ways are not flexible enough, you can implement a theme by yourself and register it using a custom theme source (see below):
The prefered way is to subclass `AbstractTheme` as you just need to implement the base colors and optionally override the extended colors - styling the controls is done by the base class.

The more advanced way is implementing the `ITheme` interface. This only supports the basic infrastructure like theme capabilities but the styling is completely in your hands.

### Add custom theme source
If you want to add another theme source besides files and resources (e.g. when implementing custom `ITheme` or `AbstractTheme` implementations) or you just want to change the folder path, you can add a custom `IThemeLookup` implementation which handles the search for available themes:
```csharp
    internal class MyThemeLookup : IThemeLookup
    {
        public int Order => 999; //highest order wins when 2 lookups return the same theme name

        public List<ITheme> Lookup()
        {
            List<ITheme> results = new List<ITheme>();
            //implement search for themes here
            return results;
        }
    }
}
```
After this, you need to register this class using `ThemeRegistry.AddThemeLookupPlugin(new MyThemeLookup());`.

**Note: This has to be done BEFORE any call to `ThemeRegistry.Current`, `ThemeRegistry.Get` or `ThemeRegistry.List` is done, otherwise the list of themes would already be created and the call to `AddThemeLookupPlugin` would fail.**

### Add third-party controls theme support
As we do not want to force you to use a specific WinForms control library, we currently only support styling of standard controls and controls from our [winforms-stylable-controls](https://github.com/Assorted-Development/winforms-stylable-controls) project.
As we understand you may want to also style other controls, we support adding specialised plugins to handle styling of a specific type of control. To do this, you need to implement ``:
```csharp
    internal class MyCustomControlThemePlugin : IThemePlugin
    {
        public void Apply(Control control, AbstractTheme theme)
        {
            MyCustomControl mcc = (MyCustomControl)control;
            //style control based on the colors available in the Theme
        }
    }
```
At last, you just need to register it for the correct type: `ThemeRegistry.AddThemePlugin<MyCustomControl>(new MyCustomControlThemePlugin());`. This will execute your code whenever a Control of the type `MyCustomControl` is detected.

**Note 1: those plugins do NOT work if you implemented a custom Theme based on `ITheme` instead of `AbstractTheme`. As you are completely free with implementing the `ITheme` interface, we expect you to handle your custom controls too.**

**Note 2: Currently, we only support directly registered types. Subclasses will not be styled!**

## Contributions

Please view the [contributing guide](/CONTRIBUTING.md) for more information.