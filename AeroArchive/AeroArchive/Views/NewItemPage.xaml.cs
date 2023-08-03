using AeroArchive.Models;
using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}