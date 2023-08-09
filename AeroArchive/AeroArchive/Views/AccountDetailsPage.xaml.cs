using AeroArchive.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountDetailsPage : ContentPage
    {
        public AccountDetailsPage()
        {
            InitializeComponent();
            BindingContext = new AccountDetailsViewModel();
        }
    }
}