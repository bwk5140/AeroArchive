using AeroArchive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmployeePage : ContentPage
    {
        public EditEmployeePage()
        {
            InitializeComponent();
            BindingContext = new EditEmployeeViewModel();
        }
    }
}