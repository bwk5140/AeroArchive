using AeroArchive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeesPage : ContentPage
    {
        EmployeesViewModel _viewModel;
        public EmployeesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EmployeesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}