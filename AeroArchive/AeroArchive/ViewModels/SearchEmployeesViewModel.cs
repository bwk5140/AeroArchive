using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class SearchEmployeesViewModel : BaseViewModel
    {
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }
        public Command AddEmployeeCommand { get; }
        public Command ClearEmployeeCommand { get; }
        public Command<Employee> ItemTapped { get; }
        public Command SearchItemCommand { get; }

        public SearchEmployeesViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());

            ItemTapped = new Command<Employee>(OnEmployeeSelected);

            SearchItemCommand = new Command(OnSearchItem);
        }

        async Task ExecuteLoadEmployeesCommand()
        {
            IsBusy = true;

            try
            {
                Employees.Clear();
                var employees = await App.Employee_Database.GetEmployeeDetsAsync();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
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
            _selectedEmployee = null;
        }

        public Employee SelectedItem
        {
            get => _selectedEmployee;
            set
            {
                SetProperty(ref _selectedEmployee, value);
                OnEmployeeSelected(value);
            }
        }

        async void OnEmployeeSelected(Employee employee)
        {
            if (employee == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(EmployeeDetailPage)}?{nameof(EmployeeDetailViewModel.ItemId)}={employee.ID}");
        }

        async void OnSearchItem(object obj)
        {
            try
            {
                Employees.Clear();
                string lowercaseName, lowercaseID, lowercaseRole;
                var text = (string)obj;
                var items = await App.Employee_Database.GetEmployeeDetsAsync();

                foreach (var item in items)
                {
                    lowercaseName = item.FullName.ToLower();
                    lowercaseID = item.EmployeeID.ToLower();
                    lowercaseRole = item.Role.ToLower();

                    if (item.FullName.Contains(text) || lowercaseName.Contains(text))
                    {
                        Employees.Add(item);
                    }
                    else if (item.EmployeeID.Contains(text) || lowercaseID.Contains(text))
                    {
                        Employees.Add(item);
                    }
                    else if (item.Role.Contains(text) || lowercaseRole.Contains(text))
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
