using AeroArchive.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string warrantyStatus;
        private Item selectedItem;
        public int ID { get; set; }
        public Command DeleteItemCommand { get; }

        public ItemDetailViewModel()
        {
            DeleteItemCommand = new Command (OnDeleteItem);
            this.PropertyChanged +=
                (_, __) => DeleteItemCommand.ChangeCanExecute();
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
                Text = item.Text;
                Description = item.Description;
                WarrantyStatus = item.WarrantyStatus;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Product");
            }
        }

        public async void OnDeleteItem()
        {
            selectedItem = await App.Prod_Database.GetProductDetsAsync(Convert.ToInt32(itemId));

            try
            {
                await App.Prod_Database.DeleteProductDetsAsync(selectedItem);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete Product");
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
