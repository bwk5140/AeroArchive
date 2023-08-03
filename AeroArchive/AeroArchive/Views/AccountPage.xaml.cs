using AeroArchive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        AccountViewModel _viewModel;
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AccountViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}