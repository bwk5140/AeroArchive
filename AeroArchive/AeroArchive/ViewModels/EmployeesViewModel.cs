using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees { get; }
        public Command LoadEmployeesCommand { get; }
        public Command AddEmployeeCommand { get; }
        public Command ClearEmployeeCommand { get; }
        public Command<Employee> ItemTapped { get; }
        public Command SearchItemCommand { get; }

        public EmployeesViewModel()
        {
            Title = "Employees";
            Employees = new ObservableCollection<Employee>();
            LoadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand());

            ItemTapped = new Command<Employee>(OnEmployeeSelected);

            AddEmployeeCommand = new Command(OnAddEmployee);

            ClearEmployeeCommand = new Command(OnClearEmployees);
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

        private async void OnAddEmployee(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewEmployeePage));
        }
        private async void OnClearEmployees()
        {
            string response;
            response = await Application.Current.MainPage.DisplayActionSheet("Clear employee database?\n\n", "No", "Yes");

            if (response != null && (response == "Clear" || response == "Yes"))
            {
                await App.Employee_Database.ClearEmployeeDBAsync();
            }
            else if (response == null || (response == "Cancel" || response == "No"))
                return;

            await ExecuteLoadEmployeesCommand();
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
            await Shell.Current.GoToAsync(nameof(SearchEmployeesPage));
        }

    }
}
