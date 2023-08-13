using AeroArchive.ViewModels;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using AeroArchive.Models;
using System.Collections.ObjectModel;

namespace AeroArchive.Views
{
    public partial class SearchProductsPage : ContentPage
    {
        readonly SearchProductsViewModel _viewModel;
        public ObservableCollection<Item> Items { get; }
        public SearchProductsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchProductsViewModel();
            Items = _viewModel.Items;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Items.Clear();
                string lowercaseItem, lowercaseWarranty, lowercaseDescription;
                var items = await App.Prod_Database.GetProductDetsAsync();

                foreach (var item in items)
                {
                    lowercaseItem = item.Text.ToLower();
                    lowercaseWarranty = item.WarrantyStatus.ToLower();
                    lowercaseDescription = item.Description.ToLower();

                    if (item.Text.Contains(e.NewTextValue) || lowercaseItem.Contains(e.NewTextValue))
                    {
                        Items.Add(item);
                    }
                    else if (item.WarrantyStatus.Contains(e.NewTextValue) || lowercaseWarranty.Contains(e.NewTextValue))
                    {
                        Items.Add(item);
                    }
                    else if (item.Description.Contains(e.NewTextValue) || lowercaseDescription.Contains(e.NewTextValue))
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