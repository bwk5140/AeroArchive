using AeroArchive.Models;
using AeroArchive.ViewModels;
using AeroArchive.AppDatabase;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RegistrationPage : ContentPage
    {
        public Command RegistrationCommand { get; }
        public string ItemId
        {
            set
            {
                LoadRegistration(value);
            }
        }
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new Registration();
            //BindingContext = new RegistrationViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve account details from the database, and set them as the
            // data source for the CollectionView.
            await App.EmployeeDatabase.GetRegistrationDetsAsync();
        }

        async void LoadRegistration(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the account and set it as the BindingContext of the page.
                Registration registration = await App.EmployeeDatabase.GetRegistrationDetsAsync(id);
                BindingContext = registration;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load account.");
            }
        }

        async void OnRegistrationClicked(object sender, EventArgs e)
        {
            var registration = (Registration)BindingContext;
            registration.Date = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(registration.Email))
            {
                // Save user details.
                await App.EmployeeDatabase.SaveRegistrationDetsAsync(registration);
            }
            // Navigate backwards
            await Shell.Current.GoToAsync("..");

        }
    }

}