using System.Drawing;
using System.Windows.Forms;

namespace MFBot_1701_E.Theming.Themes.ToolStrip;

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

    public override Color MenuItemPressedGradientBegin { get; }
    public override Color MenuItemPressedGradientEnd { get; }
    public override Color MenuItemSelectedGradientBegin { get; }
    public override Color MenuItemSelectedGradientEnd { get; }
    public override Color ToolStripBorder { get; }
    public override Color SeparatorDark { get; }
    public override Color SeparatorLight { get; }
    public override Color GripDark { get; }
    public override Color GripLight { get; }

    public override Color ImageMarginGradientBegin { get; }
    public override Color ImageMarginGradientMiddle { get; }
    public override Color ImageMarginGradientEnd { get; }
    public override Color MenuBorder { get; }
    public override Color MenuItemBorder { get; }
    public override Color ToolStripDropDownBackground { get; }
    public override Color ToolStripContentPanelGradientBegin { get; }
    public override Color ToolStripContentPanelGradientEnd { get; }
    public override Color ToolStripGradientBegin { get; }
    public override Color ToolStripGradientEnd { get; }
    public override Color ToolStripGradientMiddle { get; }
}