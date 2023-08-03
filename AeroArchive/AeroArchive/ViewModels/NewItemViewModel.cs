using AeroArchive.Models;
using System;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public string itemID;
        private string text;
        private string description;
        private string warrantyStatus;
        private Item selectedItem;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(warrantyStatus);
        }


        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string WarrantyStatus
        {
            get => warrantyStatus;
            set => SetProperty(ref warrantyStatus, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            selectedItem = await App.Prod_Database.GetProductDetsAsync(Convert.ToInt32(itemID));

            Item newItem = new Item()
            {
                Text = Text,
                Description = Description,
                WarrantyStatus = WarrantyStatus
            };

            await App.Prod_Database.SaveProductDetsAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
