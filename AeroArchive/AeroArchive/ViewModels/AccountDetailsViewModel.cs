using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class AccountDetailsViewModel : BaseViewModel
    {
        public string itemId;
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string passWord;
        public int ID { get; set; }

        public AccountDetailsViewModel()
        {
        }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        public string UserName
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Password
        {
            get => passWord;
            set => SetProperty(ref passWord, value);
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            IsBusy = true;

            try
            {
                ID = Convert.ToInt32(itemId);
                var item = await App.Account_Database.GetRegistrationDetsAsync(Convert.ToInt32(itemId));
                FirstName = item.FirstName;
                LastName = item.LastName;
                UserName = item.UserName;
                Email = item.Email;
                Password = item.Password;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
