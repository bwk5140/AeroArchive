using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command ClearItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ProductsViewModel()
        {
            Title = "Products";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            ClearItemCommand = new Command(OnClearItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.Prod_Database.GetProductDetsAsync();
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        private async void OnClearItem(object obj)
        {
            string response;
            response = await Application.Current.MainPage.DisplayActionSheet("Warning! Clear product database?", "Cancel" , "Clear", "Yes", "No");
            if (response != null && (response == "Clear" || response == "Yes"))
            {
                await App.Prod_Database.ClearProductDBAsync();
            }
            else if (response != null && (response == "Cancel" || response == "No"))
                return;
            
            await ExecuteLoadItemsCommand();
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.ID}");
        }

    }
}