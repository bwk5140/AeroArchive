using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Registration userAccount;
        private Registration _selectedItem;
        public ObservableCollection<Registration> Accounts { get; }
        public Command LoadAccountsCommand { get; }
        public Command DeleteItemCommand { get; }
        public Command<Registration> ItemTapped { get; }

        public AccountViewModel() 
        {
            Title = "Account";
            Accounts = new ObservableCollection<Registration>();
            LoadAccountsCommand = new Command(async () => await ExecuteLoadAccountsCommand());
            ItemTapped = new Command<Registration>(OnItemSelected);
            DeleteItemCommand = new Command(OnDeleteItem);
        }
        async Task ExecuteLoadAccountsCommand()
        {
            IsBusy = true;

            try
            {
                Accounts.Clear();
                var username = await SecureStorage.GetAsync("UserNametoken");
                var password = await SecureStorage.GetAsync("Passwordtoken");

                var items = await App.Account_Database.GetRegistrationDetsAsync();

                foreach (var item in items)
                {
                    if (username == item.UserName && password == item.Password)
                    {
                        Accounts.Add(item);
                        userAccount = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Registration SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        private async void OnDeleteItem(object obj)
        {
            string response;
            response = await Application.Current.MainPage.DisplayActionSheet("Delete account?", "Cancel", "Delete", "Yes", "No");
            
            if (response != null && (response == "Yes" || response == "Delete"))
            {
                await App.Account_Database.DeleteRegistrationDetsAsync(userAccount);
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else if (response != null && (response == "Cancel" || response == "No"))
                return;

            await ExecuteLoadAccountsCommand();
        }

        async void OnItemSelected(Registration item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(AccountDetailsPage)}?{nameof(AccountDetailsViewModel.ItemId)}={item.ID}");
        }
    }
}
