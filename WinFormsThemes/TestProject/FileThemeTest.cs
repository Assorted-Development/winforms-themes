using System.Drawing;
using Microsoft.Extensions.Logging;
using TestProject.Properties;
using WinFormsThemes.Extensions;
using WinFormsThemes.Themes;

namespace TestProject
{
    [TestClass]
    public class FileThemeTest : AbstractTestClass
    {
        [TestMethod]
        public void LoadShouldNotThrow_MissingCapabilities()
        {
            FileTheme? theme = FileTheme.Load(Resources.MISSING_CAPS, getLogger());
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadShouldNotThrow_MissingColors()
        {
            FileTheme? theme = FileTheme.Load(Resources.MISSING_COLORS, getLogger());
            Assert.IsNotNull(theme);
            Assert.AreEqual(SystemColors.Control, theme.BackgroundColor);
        }

        [TestMethod]
        public void LoadShouldNotThrow_MissingName()
        {
            FileTheme? theme = FileTheme.Load(Resources.MISSING_NAME, getLogger());
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadShouldNotThrow_NonJson()
        {
            FileTheme? theme = FileTheme.Load("abc", getLogger());
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadVersionedSimple()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast'],
	                'version': 3,
                    'variables': {
      
                    },
	                'colors': {
		                'backColor': '#082a56',
		                'foreColor': '#082a56',
                        'controls': {
			                'backColor': '#082a56',
			                'foreColor': '#082a56'
                        }
	                }
                }".Replace("'", "\"", StringComparison.Ordinal), getLogger());
            Assert.IsNotNull(theme);
        }

        [TestMethod]
        public void LoadVersionedWithVariable()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast'],
	                'version': 3,
                    'variables': {
                        'backColor': '#082a56',
                        'foreColor': '#082a57'
                    },
	                'colors': {
		                'backColor': 'backColor',
		                'foreColor': 'foreColor',
                        'controls': {
			                'backColor': 'backColor',
			                'foreColor': 'foreColor'
                        }
	                }
                }".Replace("'", "\"", StringComparison.CurrentCulture), getLogger());
            Assert.IsNotNull(theme);
            Assert.AreEqual("#082a56".ToColor(), theme.ControlBackColor);
            Assert.AreEqual("#082a57".ToColor(), theme.ControlForeColor);
        }

        [TestMethod]
        public void LoadVersionedInvalidSchema()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast'],
	                'version': 3,
                    'variables': {
      
                    }
                }".Replace("'", "\"", StringComparison.CurrentCulture), getLogger());
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadVersionedDefaultValue()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast'],
	                'version': 3,
                    'variables': {
      
                    },
	                'colors': {
		                'backColor': '#082a56',
		                'foreColor': '#082a56',
                        'controls': {
			                'backColor': '#082a56',
			                'foreColor': '#082a56'
                        }
	                }
                }".Replace("'", "\"", StringComparison.CurrentCulture), getLogger());
            Assert.IsNotNull(theme);
            Assert.AreEqual("#082a56".ToColor(), theme.TableBackColor);
        }

        [TestMethod]
        public void LoadVersionedInvalidColorOrVariable()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast'],
	                'version': 3,
                    'variables': {
      
                    },
	                'colors': {
		                'backColor': 'unknown',
		                'foreColor': '#082a56',
                        'controls': {
			                'backColor': '#082a56',
			                'foreColor': '#082a56'
                        }
	                }
                }".Replace("'", "\"", StringComparison.CurrentCulture), getLogger());
            Assert.IsNull(theme);
        }

        [TestMethod]
        public void LoadVersionedSkipEmptyCapabilities()
        {
            FileTheme? theme = FileTheme.Load(@"
                {
	                'name': 'theme-name',
	                'capabilities': ['DarkMode', 'HighContrast', ''],
	                'version': 3,
                    'variables': {
      
                    },
	                'colors': {
		                'backColor': '#082a56',
		                'foreColor': '#082a56',
                        'controls': {
			                'backColor': '#082a56',
			                'foreColor': '#082a56'
                        }
	                }
                }".Replace("'", "\"", StringComparison.Ordinal), getLogger());
            Assert.IsNotNull(theme);
            Assert.AreEqual(0, theme.AdvancedCapabilities.Count);
        }

        private ILogger getLogger()
        {
            return new Logger<FileThemeTest>(LoggerFactory);
        }
    }
}
