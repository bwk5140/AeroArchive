using AeroArchive.Models;
using AeroArchive.ViewModels;
using System.Diagnostics;
using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace AeroArchive.Views
{
    public partial class SearchEmployeesPage : ContentPage
    {
        readonly SearchEmployeesViewModel _viewModel;
        public ObservableCollection<Employee> Employees { get; }
        public SearchEmployeesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchEmployeesViewModel();
            Employees = _viewModel.Employees;
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
                Employees.Clear();
                string lowercaseName, lowercaseID, lowercaseRole, lowercaseEmail;
                var items = await App.Employee_Database.GetEmployeeDetsAsync();

                foreach (var item in items)
                {
                    lowercaseName = item.FullName.ToLower();
                    lowercaseID = item.EmployeeID.ToLower();
                    lowercaseRole = item.Role.ToLower();
                    lowercaseEmail = item.Email.ToLower();

                    if (item.FullName.Contains(e.NewTextValue) || lowercaseName.Contains(e.NewTextValue))
                    {
                        Employees.Add(item);
                    }
                    else if (item.EmployeeID.Contains(e.NewTextValue) || lowercaseID.Contains(e.NewTextValue))
                    {
                        Employees.Add(item);
                    }
                    else if (item.Role.Contains(e.NewTextValue) || lowercaseRole.Contains(e.NewTextValue))
                    {
                        Employees.Add(item);
                    }
                    else if (item.Email.Contains(e.NewTextValue) || lowercaseEmail.Contains(e.NewTextValue))
                    {
                        Employees.Add(item);
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