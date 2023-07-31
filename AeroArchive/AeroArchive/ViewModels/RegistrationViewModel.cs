using AeroArchive.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class RegistrationViewModel
    {
        public Command RegistrationCommand { get; }


        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(OnRegistrationClicked);
        }

        private async void OnRegistrationClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

}