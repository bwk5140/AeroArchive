using AeroArchive.ViewModels;
using System;
using Xamarin.Forms;

namespace AeroArchive.Views
{
    public partial class ProductsPage : ContentPage
    {
        ProductsViewModel _viewModel;
        ToolbarItem clearButton;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProductsViewModel();

            clearButton = new ToolbarItem
            {
                Text = "Clear",
                Command = _viewModel.ClearItemCommand
            };

            // If the user is an admin, add the adminItem to the Toolbar
            ShowAdminToolbarItemIfNeeded();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.OnAppearing();

            // When the page appears, check again if the user is an admin and show or hide the ToolbarItem accordingly
            ShowAdminToolbarItemIfNeeded();
        }

        private async void ShowAdminToolbarItemIfNeeded()
        {
            if (Application.Current.Properties.ContainsKey("LoggedInUserId"))
            {
                var userId = (int)Application.Current.Properties["LoggedInUserId"];
                var user = await App.EmployeeDatabase.GetRegistrationDetsAsync(userId);

                if (user != null && user.Admin)
                {
                    if (!this.ToolbarItems.Contains(clearButton))
                    {
                        this.ToolbarItems.Add(clearButton);
                    }
                }
                else
                {
                    if (this.ToolbarItems.Contains(clearButton))
                    {
                        this.ToolbarItems.Remove(clearButton);
                    }
                }
            }
        }
    }
}