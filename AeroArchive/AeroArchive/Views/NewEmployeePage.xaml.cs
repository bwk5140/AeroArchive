using AeroArchive.Models;
using AeroArchive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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