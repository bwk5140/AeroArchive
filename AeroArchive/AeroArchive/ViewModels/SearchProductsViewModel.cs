using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class SearchProductsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public ObservableCollection<Item> Items { get; }
        public Command SearchItemCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public SearchProductsViewModel()
        {
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SearchItemCommand = new Command(OnSearchItem);
            ItemTapped = new Command<Item>(OnItemSelected);
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
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.ID}");
        }

        async void OnSearchItem(object obj)
        {
            try
            {
                Items.Clear();
                string lowercaseItem, lowercaseWarranty, lowercaseDescription;
                var text = (string)obj;
                var items = await App.Prod_Database.GetProductDetsAsync();

                foreach (var item in items)
                {
                    lowercaseItem = item.Text.ToLower();
                    lowercaseWarranty = item.WarrantyStatus.ToLower();
                    lowercaseDescription = item.Description.ToLower();

                    if (item.Text.Contains(text) || lowercaseItem.Contains(text))
                    {
                        Items.Add(item);
                    }
                    else if (item.WarrantyStatus.Contains(text) || lowercaseWarranty.Contains(text))
                    {
                        Items.Add(item);
                    }
                    else if (item.Description.Contains(text) || lowercaseDescription.Contains(text))
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
