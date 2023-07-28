using System.Diagnostics.CodeAnalysis;

namespace WinFormsThemes.Themes.ToolStrip;

internal class ThemedColorTable : ProfessionalColorTable
{
    public ThemedColorTable(Color toolStripBorderColor, Color separatorColor, Color menuItemHoverColor,
        Color menuItemPressedColor, Color controlBackColor)
    {
        ToolStripBorder = toolStripBorderColor;
        SeparatorDark = SeparatorLight = GripDark = GripLight = separatorColor;
        MenuItemSelectedGradientBegin = MenuItemSelectedGradientEnd = menuItemHoverColor;
        MenuItemPressedGradientBegin = MenuItemPressedGradientEnd = menuItemPressedColor;

        ImageMarginGradientBegin = ImageMarginGradientMiddle = ImageMarginGradientEnd =
            MenuBorder = MenuItemBorder = ToolStripDropDownBackground = ToolStripGradientBegin =
                ToolStripGradientEnd = ToolStripGradientMiddle = ToolStripContentPanelGradientBegin =
                    ToolStripContentPanelGradientEnd = controlBackColor;
    }

    [ExcludeFromCodeCoverage]
    public override Color GripDark { get; }

    [ExcludeFromCodeCoverage]
    public override Color GripLight { get; }

    [ExcludeFromCodeCoverage]
    public override Color ImageMarginGradientBegin { get; }

    [ExcludeFromCodeCoverage]
    public override Color ImageMarginGradientEnd { get; }

    [ExcludeFromCodeCoverage]
    public override Color ImageMarginGradientMiddle { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuBorder { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuItemBorder { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuItemPressedGradientBegin { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuItemPressedGradientEnd { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuItemSelectedGradientBegin { get; }

    [ExcludeFromCodeCoverage]
    public override Color MenuItemSelectedGradientEnd { get; }

    [ExcludeFromCodeCoverage]
    public override Color SeparatorDark { get; }

    [ExcludeFromCodeCoverage]
    public override Color SeparatorLight { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripBorder { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripContentPanelGradientBegin { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripContentPanelGradientEnd { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripDropDownBackground { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripGradientBegin { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripGradientEnd { get; }

    [ExcludeFromCodeCoverage]
    public override Color ToolStripGradientMiddle { get; }
}