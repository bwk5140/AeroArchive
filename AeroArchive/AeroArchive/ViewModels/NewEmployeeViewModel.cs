using AeroArchive.Models;
using System;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class NewEmployeeViewModel : BaseViewModel
    {
        public string itemId;
        private string fullName;
        private string employeeID;
        private string role;
        private string email;
        private Employee selectedEmployee;

        public NewEmployeeViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(fullName)
                && !String.IsNullOrWhiteSpace(employeeID)
                && !String.IsNullOrWhiteSpace(role)
                && !String.IsNullOrWhiteSpace(email);
        }
        public string Fullname
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        public string EmployeeID
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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            selectedEmployee = await App.Employee_Database.GetEmployeeDetsAsync(Convert.ToInt32(itemId));

            Employee newEmployee = new Employee()
            {
                FullName = Fullname,
                EmployeeID = EmployeeID,
                Role = Role, 
                Email = Email
            };

            await App.Employee_Database.SaveEmployeeDetsAsync(newEmployee);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
