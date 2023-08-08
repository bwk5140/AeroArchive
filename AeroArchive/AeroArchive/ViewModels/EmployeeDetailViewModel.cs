using AeroArchive.Models;
using AeroArchive.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EmployeeDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string fullName;
        private string employeeID;
        private string role;
        public string email;
        private Employee selectedEmployee;
        public int ID { get; set; }
        public Command EditEmployeeCommand { get; }
        public Command DeleteEmployeeCommand { get; }

        public EmployeeDetailViewModel()
        {
            EditEmployeeCommand = new Command(OnEditEmployee);
            DeleteEmployeeCommand = new Command(OnDeleteEmployee);
            this.PropertyChanged +=
                (_, __) => EditEmployeeCommand.ChangeCanExecute();
        }

        public string Fullname
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        public string EmployeeiD
        {
            get => employeeID;
            set => SetProperty(ref employeeID, value);
        }
        public string Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
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
                LoadEmployeeId(value);
            }
        }


        public async void LoadEmployeeId(string itemId)
        {
            try
            {
                ID = Convert.ToInt32(itemId);
                var employee = await App.Employee_Database.GetEmployeeDetsAsync(ID);
                selectedEmployee = employee;
                Fullname = selectedEmployee.FullName;
                EmployeeiD = selectedEmployee.EmployeeID;
                Role = selectedEmployee.Role;
                Email = selectedEmployee.Email;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Employee");
            }
        }
        public async void OnEditEmployee()
        {
            await Shell.Current.GoToAsync($"{nameof(EditEmployeePage)}?{nameof(EditEmployeeViewModel.ItemId)}={selectedEmployee.ID}");
        }

        public async void OnDeleteEmployee()
        {
            selectedEmployee = await App.Employee_Database.GetEmployeeDetsAsync(Convert.ToInt32(itemId));

            try
            {
                await App.Employee_Database.DeleteEmployeeDetsAsync(selectedEmployee);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete Employee");
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
