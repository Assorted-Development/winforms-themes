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
  * [Development](#development)
  * [Usage](#usage)
  * [Extended Usage](#extended-usage)
  * [Contributions](#contributions)

## Development

### Test Coverage
First, install the reportgenerator tool:
```
dotnet tool install -g dotnet-reportgenerator-globaltool
```

The test coverage report can then be created using:
```
rmdir /s /q WinFormsThemes\TestProject\TestResults
dotnet test WinFormsThemes/TestProject --no-build --verbosity normal --collect:"XPlat Code Coverage"
reportgenerator -reports:WinFormsThemes\TestProject\TestResults\*\coverage.cobertura.xml -targetdir:WinFormsThemes\TestProject\TestResults\html -reporttypes:Html -sourcedirs:WinFormsThemes\WinFormsThemes
```


## Usage
To use this project, you need to add a reference to our nuget package (`dotnet add package AssortedDevelopment.WinFormsThemes`) first.

Next, you need to configure the themes:
```csharp
IThemeRegistry.BUILDER.Build();
```
This uses the default settings to lookup the themes and register the theme in the `ThemeRegistryHolder`.

At last, you need to add a single line in the `OnLoad` in all forms to be themed:
```csharp
ThemeRegistryHolder.ThemeRegistry.Current.Apply(this);
```

This line of code will:
1. check the operating system for the user settings on dark mode and high contrast
2. Look for a theme matching the settings for dark mode and high contrast
3. use the first theme found as your current theme

Last but not least, this will also apply this theme on the given Form and all children.

## Extended Usage
Of course, you can extend this library and customize the handling to fit your needs. Here are a few examples:

### Customize theme selection
By default, our library will honor the settings of the operating system in regard to dark mode and high contrast. If you want to add additional selection criteria or you want to give the user an option to override this selection you can do that easily.
Instead of relying on the default settings in `IThemeRegistry.Get()` which is implicitly called by `IThemeRegistry.Current` when no current theme was set you can set `IThemeRegistry.Current` to any theme you want:
```csharp
List<ITheme> allThemes = ThemeRegistryHolder.ThemeRegistry.List();
ITheme selectedTheme = null;
//logic to select theme here
ThemeRegistryHolder.ThemeRegistry.Current = selectedTheme;
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

The views can be added by either implementing an `IThemeLookup` (see below) or by adding it directly to the builder:
```csharp
IThemeRegistry.BUILDER
    .WithThemes()
        .AddDefaultThemes()
        .AddTheme(new MySuperDarkTheme())
        .CompleteThemeList()
    .Build();
```

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
After this, you need to register this class in the builder:
```csharp
 IThemeRegistry.BUILDER
     .WithThemes()
         .AddDefaultThemes()
         .FromLookup()
         .CompleteThemeList()
     .Build();
```

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
At last, you just need to register it for the correct type:
```csharp
 IThemeRegistry.BUILDER
     .AddThemePlugin<MyCustomControl>(new MyCustomControlThemePlugin())
     .Build();
```

**Note: Currently, we only support directly registered types. Subclasses will not be styled!**

## Contributions

Please view the [contributing guide](/CONTRIBUTING.md) for more information.