using AeroArchive.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

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
            await App.Account_Database.GetRegistrationDetsAsync();
        }

        async void LoadRegistration(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the account and set it as the BindingContext of the page.
                Registration registration = await App.Account_Database.GetRegistrationDetsAsync(id);
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

            var isValid = true;
            if (!string.IsNullOrWhiteSpace(registration.FirstName)
                && !string.IsNullOrWhiteSpace(registration.LastName)
                && !string.IsNullOrWhiteSpace(registration.UserName)
                && !string.IsNullOrWhiteSpace(registration.Email)
                && !string.IsNullOrWhiteSpace(registration.Password))
            {
                var items = await App.Account_Database.GetRegistrationDetsAsync();
                foreach (var item in items)
                {
                    if (item.UserName == registration.UserName)
                    {
                        await DisplayAlert("Username is already taken", "", "Ok");
                        VisualStateManager.GoToState(UserNameEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(UserNameEntry, "Valid");
                    }

                    if (item.Email == registration.Email)
                    {
                        await DisplayAlert("Email is already registered", "", "Ok");
                        VisualStateManager.GoToState(EmailEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(EmailEntry, "Valid");
                    }
                }
            }
            if (isValid)
            {
                try
                {
                    // Save user details.
                    await App.Account_Database.SaveRegistrationDetsAsync(registration);

                    // Navigate backwards
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }

}