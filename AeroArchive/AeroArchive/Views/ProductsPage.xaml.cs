using AeroArchive.ViewModels;
using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    public partial class ProductsPage : ContentPage
    {
        ProductsViewModel _viewModel;

        public ProductsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProductsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}