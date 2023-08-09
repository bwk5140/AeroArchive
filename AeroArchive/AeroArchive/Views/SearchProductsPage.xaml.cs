using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class SearchProductsPage : ContentPage
    {
        readonly SearchProductsViewModel _viewModel;
        public SearchProductsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchProductsViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}