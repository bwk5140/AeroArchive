using AeroArchive.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditItemViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string warrantyStatus;
        private Item selectedItem;
        public int ID { get; set; }
        public Command DoneEditingCommand { get; }

        public EditItemViewModel()
        {
            DoneEditingCommand = new Command(OnDoneEditingItem);
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
            try
            {
                ID = Convert.ToInt32(itemId);
                var item = await App.Prod_Database.GetProductDetsAsync(ID);
                selectedItem = item;
                Text = item.Text;
                Description = item.Description;
                WarrantyStatus = item.WarrantyStatus;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Product");
            }
        }
        public async void OnDoneEditingItem()
        {
            selectedItem = await App.Prod_Database.GetProductDetsAsync(Convert.ToInt32(itemId));
            {
                selectedItem.Text = Text;
                selectedItem.Description = Description;
                selectedItem.WarrantyStatus = WarrantyStatus;
            };

            await App.Prod_Database.SaveProductDetsAsync(selectedItem);
            await Shell.Current.GoToAsync("..");
        }
    }
}
