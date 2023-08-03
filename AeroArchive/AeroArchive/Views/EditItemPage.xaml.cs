using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class EditItemPage : ContentPage
    {
        public EditItemPage()
        {
            InitializeComponent();
            BindingContext = new EditItemViewModel();
        }
    }
}