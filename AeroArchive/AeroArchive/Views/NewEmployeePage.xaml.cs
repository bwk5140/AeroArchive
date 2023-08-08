using AeroArchive.Models;
using AeroArchive.ViewModels;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class NewEmployeePage : ContentPage
    {
        public Employee Employee { get; set; }
        public NewEmployeePage()
        {
            InitializeComponent();
            BindingContext = new NewEmployeeViewModel();
        }
    }
}