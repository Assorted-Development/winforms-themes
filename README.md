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

Next, build a debug version of the project:
```
dotnet build WinFormsThemes/WinFormsThemes.sln -c Debug
```

The test coverage report can then be created using:
```
rmdir /s /q WinFormsThemes\TestProject\TestResults
dotnet test WinFormsThemes/TestProject --no-build --verbosity normal --collect:"XPlat Code Coverage"
reportgenerator -reports:WinFormsThemes\TestProject\TestResults\*\coverage.cobertura.xml -targetdir:WinFormsThemes\TestProject\TestResults\html -reporttypes:Html -sourcedirs:WinFormsThemes\WinFormsThemes
start "" WinFormsThemes\TestProject\TestResults\html\index.html
```

### Mutation testing
We use [Stryker.NET](https://stryker-mutator.io/) for mutation testing. To run the mutation tests, use:
```
dotnet tool restore
dotnet stryker
```

**Important: Do not start stryker in the project directory - you need to start it in the solution dir, otherwise the config will not be found!**

## Usage
To use this project, you need to add a reference to our nuget package (`dotnet add package AssortedDevelopment.WinFormsThemes`) first.

**Note: Currently, this project requires .NET 6.0 or higher.**

Next, you need to configure the themes:
```csharp
var registry = ThemeRegistryHolder.GetBuilder().Build();
var theme = registry.ThemeRegistry.GetTheme();
```
This can, for example, be placed in the `Program.cs` of your application and uses the default settings to lookup the themes, return the registry and use its standard theme.

At last, you need to provide the theme to all forms to be themed and add a single line in the `Load` event:
```csharp
theme.Apply(this);
```

This will apply this theme on the given Form and all children.

## Extended Usage
Of course, you can extend this library and customize the handling to fit your needs. Here are a few examples:

### Logging
If you want to debug an issue with this library, you can enable logging in the `IThemeRegistryBuilder`:
```csharp
ThemeRegistryHolder.GetBuilder().SetLoggerFactory(LoggerFactory).Build();
```
This will log all actions of the library to the given `ILoggerFactory`.

**Note: Any calls before calling `SetLoggerFactory` will not be affected so we advise to call `SetLoggerFactory` as early as possible.**

### Making IThemeRegistry and ITheme globally available
When you do not have a dependency injection available in your project, we provide utilities to make both `IThemeRegistry` and `ITheme` globally available:
1. `IThemeRegistry`
For the `IThemeRegistry`, we provide the `ThemeRegistryHolder` class which can be used to store the registry and retrieve it later:
```csharp
ThemeRegistryHolder.ThemeRegistry = ThemeRegistryHolder.GetBuilder().Build();
```
After this, you can retrieve the registry from anywhere in your application using: `var registry = ThemeRegistryHolder.ThemeRegistry;`

2. `ITheme`
For the `ITheme`, the `IThemeRegistry` provides a `Current` property which can be used to retrieve the current theme. For this to work though, you need to configure a selector that defines the current theme:
```csharp
private ITheme SelectCurrentTheme(IThemeRegistry registry)
{
	//logic to select theme here
}
...
ThemeRegistryHolder.ThemeRegistry =  ThemeRegistryHolder.GetBuilder().WithCurrentThemeSelector(SelectCurrentTheme).Build();
```

This enables you to use `IThemeRegistry.Current` to retrieve the current theme and `IThemeRegistry.OnThemeChanged` to be notified of changes:
```csharp
var mytheme = ThemeRegistryHolder.ThemeRegistry.Current;
ThemeRegistryHolder.ThemeRegistry.OnThemeChanged += (sender, args) =>
{
	//logic to handle theme change here
};
```

### Customize theme selection
By default, our library will honor the settings of the operating system in regard to dark mode and high contrast when calling `GetTheme`. If you want to add additional selection criteria or you want to give the user an option to override this selection you can do that easily.
Instead of relying on the default settings in `IThemeRegistry.GetTheme()` you can set `IThemeRegistry.Current` to any theme you want by providing a `CurrentThemeSelector`:
```csharp
IThemeRegistry registry = ThemeRegistryHolder.GetBuilder()
                .WithCurrentThemeSelector(registry => registry.GetTheme())
				.Build();
var selectedTheme = registry.CurrentTheme;
```

### Add custom themes
Out of the box, there are 2 ways you can add custom themes:
- Files with the file ending `.theme.json` stored in a `themes` directory of the working dir.
- Assembly resources in any assembly where the name starts with `CONFIG_THEMING_THEME_`

Both ways use the same JSON format for the theme definition(the version defines the format of the file).
A simple example of this could be:  
```json
{
    "name": "theme-name",
    "capabilities": ["DarkMode", "HighContrast"],
    "version": 3,
    "variables": {
        "backColor": "#082a56",
        "foreColor": "#082a57"
    },
    "colors": {
        "backColor": "backColor",
        "foreColor": "foreColor",
        "controls": {
            "backColor": "backColor",
            "foreColor": "foreColor"
        }
    }
}
```
For the complete list of available settings please check our JSON schema [here](https://github.com/Assorted-Development/winforms-themes/blob/main/WinFormsThemes/WinFormsThemes/themes.schema.json).

If those 2 ways are not flexible enough, you can implement a theme by yourself and register it using a custom theme source (see below):
The prefered way is to subclass `AbstractTheme` as you just need to implement the base colors and optionally override the extended colors - styling the controls is done by the base class.

The more advanced way is implementing the `ITheme` interface. This only supports the basic infrastructure like theme capabilities but the styling is completely in your hands.

The views can be added by either implementing an `IThemeLookup` (see below) or by adding it directly to the builder:
```csharp
ThemeRegistryHolder.GetBuilder()
    .WithThemes()
        .AddDefaultThemes()
        .AddTheme(new MySuperDarkTheme())
        .FinishThemeList()
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
 ThemeRegistryHolder.GetBuilder()
     .WithThemes()
         .AddDefaultThemes()
         .WithLookup()
         .FinishThemeList()
     .Build();
```

### Add third-party controls theme support
As we do not want to force you to use a specific WinForms control library, we currently only support styling of standard controls and controls from our [winforms-stylable-controls](https://github.com/Assorted-Development/winforms-stylable-controls) project.
As we understand you may want to also style other controls, we support adding specialised plugins to handle styling of a specific type of control. To do this, you need to implement ``:
```csharp
    internal class MyCustomControlThemePlugin : AbstractThemePlugin<MyCustomControl>
    {
        protected override void ApplyPlugin(MyCustomControl mcc, AbstractTheme theme)
        {
            //style control based on the colors available in the Theme
        }
    }
```
At last, you just need to register it for the correct type:
```csharp
 ThemeRegistryHolder.GetBuilder()
     .AddThemePlugin(new MyCustomControlThemePlugin())
     .Build();
```

**Note: Currently, we only support directly registered types. Subclasses will not be styled automatically!**

## Contributions

Please view the [contributing guide](/CONTRIBUTING.md) for more information.