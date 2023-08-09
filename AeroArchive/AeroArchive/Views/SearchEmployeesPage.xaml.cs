using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class SearchEmployeesPage : ContentPage
    {
        readonly SearchEmployeesViewModel _viewModel;
        public SearchEmployeesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchEmployeesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}