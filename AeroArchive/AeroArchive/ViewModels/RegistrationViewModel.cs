using AeroArchive.Views;
using Xamarin.Forms;
using AeroArchive.Models;
using System;

namespace AeroArchive.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private bool _isAdmin;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private string _password;

        public Command RegistrationCommand { get; }

        // Property for CheckBox binding
        public bool IsAdmin
        {
            get => _isAdmin;
            set => SetProperty(ref _isAdmin, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(OnRegistrationClicked);
        }

        async void OnRegistrationClicked()
        {
            var newUser = new Registration
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password,
                Admin = IsAdmin,
                Date = DateTime.Now
            };

            // Insert new user in database
            await App.EmployeeDatabase.SaveRegistrationDetsAsync(newUser);

            // Navigate to HomePage
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}