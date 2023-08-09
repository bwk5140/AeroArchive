using AeroArchive.Models;
using AeroArchive.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class ProductsPage : ContentPage
    {
        readonly ProductsViewModel _viewModel;
        
        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProductsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}