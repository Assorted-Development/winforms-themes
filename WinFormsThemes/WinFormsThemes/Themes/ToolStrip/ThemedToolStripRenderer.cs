using System.Drawing.Imaging;
using System.Drawing.Text;

namespace WinFormsThemes.Themes.ToolStrip;

internal class ThemedToolStripRenderer : ToolStripProfessionalRenderer
{
    private Color TextColorEnabled { get; }
    private Color TextColorDisabled { get; }

    public ThemedToolStripRenderer(
        ProfessionalColorTable professionalColorTable, Color textColorEnabled, Color textColorDisabled)
        : base(professionalColorTable)
    {
        TextColorEnabled = textColorEnabled;
        TextColorDisabled = textColorDisabled;
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        ToolStripItem item = e.Item;
        Graphics g = e.Graphics;
        Font textFont = e.TextFont;
        string text = e.Text;
        Rectangle textRect = e.TextRectangle;
        TextFormatFlags textFormat = e.TextFormat;

        // if we're disabled draw in a different color.
        Color textColor = item.Enabled ? TextColorEnabled : TextColorDisabled;

        if (e.TextDirection != ToolStripTextDirection.Horizontal && textRect is { Width: > 0, Height: > 0 })
        {
            // Perf: this is a bit heavy handed.. perhaps we can share the bitmap.
            Size textSize = FlipSize(textRect.Size);
            using (Bitmap textBmp = new(textSize.Width, textSize.Height, PixelFormat.Format32bppPArgb))
            {
                using (Graphics textGraphics = Graphics.FromImage(textBmp))
                {
                    // now draw the text..
                    textGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    TextRenderer.DrawText(textGraphics, text, textFont, new Rectangle(Point.Empty, textSize), textColor, textFormat);
                    textBmp.RotateFlip((e.TextDirection == ToolStripTextDirection.Vertical90) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(textBmp, textRect);
                }
            }
        }
        else
        {
            TextRenderer.DrawText(g, text, textFont, textRect, textColor, textFormat);
        }
    }

    private static Size FlipSize(Size size)
    {
        // Size is a struct (passed by value, no need to make a copy)
        (size.Width, size.Height) = (size.Height, size.Width);
        return size;
    }
}