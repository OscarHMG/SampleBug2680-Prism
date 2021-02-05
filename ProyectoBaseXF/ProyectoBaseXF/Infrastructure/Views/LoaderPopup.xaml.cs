using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ProyectoBaseXF.Infrastructure.Views
{
    public partial class LoaderPopup : PopupPage
    {
        public LoaderPopup()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
