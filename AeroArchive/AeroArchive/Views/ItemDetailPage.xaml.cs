using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}