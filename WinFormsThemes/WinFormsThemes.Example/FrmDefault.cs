using WinFormsThemes;

namespace StylableWinFormsControls.Example
{
    public partial class FrmDefault : Form
    {
        public FrmDefault()
        {
            InitializeComponent();
            //ThemeRegistryHolder.ThemeRegistry.Current.Apply(this);
            ThemeRegistryHolder.ThemeRegistry.Get(ThemeCapabilities.HighContrast).Apply(this);
        }

        private void stylableListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}