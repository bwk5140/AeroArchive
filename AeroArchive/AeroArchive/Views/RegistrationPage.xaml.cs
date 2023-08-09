using AeroArchive.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using System.ComponentModel;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage, INotifyPropertyChanged
    {
        public Command RegistrationCommand { get; }
        public bool IsValidated { get; set; }
        
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new Registration();
        }

        private async void SignInTapped(object sender, EventArgs e)
        {
            // Navigate backwards
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve account details from the database, and set them as the
            // data source for the CollectionView.
            await App.Account_Database.GetRegistrationDetsAsync();
        }
        
        async void OnRegistrationClicked(object sender, EventArgs e)
        {
            var registration = (Registration)BindingContext;
            registration.Date = DateTime.UtcNow;

            var isValid = true;

            if (string.IsNullOrWhiteSpace(FirstNameEntry.Text) || FirstNameEntry.Text.Length < 2)
            {
                VisualStateManager.GoToState(FirstNameEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(FirstNameEntry, "Valid");
            }
            if (string.IsNullOrWhiteSpace(LastNameEntry.Text) || LastNameEntry.Text.Length < 2)
            {
                VisualStateManager.GoToState(LastNameEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(LastNameEntry, "Valid");
            }
            if (string.IsNullOrWhiteSpace(UserNameEntry.Text) || UserNameEntry.Text.Length < 5)
            {
                VisualStateManager.GoToState(UserNameEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(UserNameEntry, "Valid");
            }
            if (string.IsNullOrWhiteSpace(EmailEntry.Text) || EmailEntry.Text.Length < 6)
            {
                VisualStateManager.GoToState(EmailEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(EmailEntry, "Valid");
            }
            if (string.IsNullOrWhiteSpace(PasswordEntry.Text) || PasswordEntry.Text.Length < 5)
            {
                VisualStateManager.GoToState(PasswordEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(PasswordEntry, "Valid");
            }
            if (string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text) || ConfirmPasswordEntry.Text.Length < 5)
            {
                VisualStateManager.GoToState(ConfirmPasswordEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(ConfirmPasswordEntry, "Valid");
            }
            if (isValid)
            {
                var items = await App.Account_Database.GetRegistrationDetsAsync();
                foreach (var item in items)
                {
                    if (item.UserName == registration.UserName && item.Email == registration.Email)
                    {
                        await DisplayAlert("User is already registered", "", "Ok");
                        VisualStateManager.GoToState(UserNameEntry, "Invalid");
                        VisualStateManager.GoToState(EmailEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(UserNameEntry, "Valid");
                        VisualStateManager.GoToState(EmailEntry, "Valid");
                    }

                    if (item.UserName == registration.UserName && isValid)
                    {
                        await DisplayAlert("Username is already taken", "", "Ok");
                        VisualStateManager.GoToState(UserNameEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(UserNameEntry, "Valid");
                    }

                    if (item.Email == registration.Email && isValid)
                    {
                        await DisplayAlert("Email is already registered", "", "Ok");
                        VisualStateManager.GoToState(EmailEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(EmailEntry, "Valid");
                    }
                    if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
                    {
                        VisualStateManager.GoToState(PasswordEntry, "Invalid");
                        VisualStateManager.GoToState(ConfirmPasswordEntry, "Invalid");
                        isValid = false;
                    }
                    else
                    {
                        VisualStateManager.GoToState(PasswordEntry, "Valid");
                        VisualStateManager.GoToState(ConfirmPasswordEntry, "Valid");
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

        void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            VisualStateManager.GoToState(FirstNameEntry, "Valid");
            VisualStateManager.GoToState(LastNameEntry, "Valid");
            VisualStateManager.GoToState(UserNameEntry, "Valid");
            VisualStateManager.GoToState(EmailEntry, "Valid");
            VisualStateManager.GoToState(PasswordEntry, "Valid");
            VisualStateManager.GoToState(ConfirmPasswordEntry, "Valid");
        }
    }

}