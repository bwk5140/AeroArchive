using AeroArchive.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Registration _selectedAccount;
        public ObservableCollection<Registration> Accounts { get; }
        public Command LoadAccountsCommand { get; }
        public Command ClearItemCommand { get; }
        public AccountViewModel() 
        {
            Title = "Accounts";
            Accounts = new ObservableCollection<Registration>();
            LoadAccountsCommand = new Command(async () => await ExecuteLoadAccountsCommand());
            ClearItemCommand = new Command(OnClearItem);
        }

        async Task ExecuteLoadAccountsCommand()
        {
            IsBusy = true;

            try
            {
                Accounts.Clear();
                var items = await App.EmployeeDatabase.GetRegistrationDetsAsync();
                foreach (var item in items)
                {
                    Accounts.Add(item);
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
            _selectedAccount = null;
        }

        private async void OnClearItem(object obj)
        {
            string response;
            response = await Application.Current.MainPage.DisplayActionSheet("Warning! Clear product database?", "Cancel", "Clear", "Yes", "No");
            if (response != null && (response == "Clear" || response == "Yes"))
            {
                await App.EmployeeDatabase.ClearAccountsDBAsync();
            }
            else if (response != null && (response == "Cancel" || response == "No"))
                return;

            await ExecuteLoadAccountsCommand();
        }
        /*
        public Registration _selectedAccount
        {
            get => _selectedAccount;
            set
            {
                SetProperty(ref _selectedAccount, value);
                OnAccountSelected(value);
            }
        }
        */
    }
}
