using System.Drawing;
using WinFormsThemes.Extensions;

namespace TestProject
{
    [TestClass]
    public class ColorExtensionsTest
    {
        [TestMethod]
        public void ToColorReturnsControlForNullString()
        {
            string? hexColor = null;
            Assert.AreEqual(SystemColors.Control, hexColor.ToColor());
        }

        [TestMethod]
        public void ToColorTest()
        {
            Assert.AreEqual(Color.FromArgb(255, 0, 0), "#FF0000".ToColor());
        }
    }
}