using System.Drawing;
using TestProject.Properties;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class FileThemeTest
    {
        [TestMethod]
        public void LoadShouldNotThrow_MissingCapabilities()
        {
            var theme = FileTheme.Load(Resources.MISSING_CAPS);
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadShouldNotThrow_MissingColors()
        {
            var theme = FileTheme.Load(Resources.MISSING_COLORS);
            Assert.IsNotNull(theme);
            Assert.AreEqual(SystemColors.Control, theme.BackgroundColor);
        }

        [TestMethod]
        public void LoadShouldNotThrow_MissingName()
        {
            var theme = FileTheme.Load(Resources.MISSING_NAME);
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadShouldNotThrow_NonJson()
        {
            var theme = FileTheme.Load("abc");
            Assert.IsNull(theme);
        }
    }
}