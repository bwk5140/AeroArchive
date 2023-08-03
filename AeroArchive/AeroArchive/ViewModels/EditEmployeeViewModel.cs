using AeroArchive.Models;
using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace AeroArchive.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditEmployeeViewModel : BaseViewModel
    {
        public string itemId;
        private string fullName;
        private string employeeID;
        private string role;
        public string email;
        private Employee selectedEmployee;
        public Command DoneEditingCommand { get; }

        public EditEmployeeViewModel()
        {
            DoneEditingCommand = new Command(OnDoneEditingEmployee);
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
                LoadEmployee(value);
            }
        }
        public async void LoadEmployee(string itemId)
        {
            try
            {
                var item = await App.Employee_Database.GetEmployeeDetsAsync(Convert.ToInt32(itemId));
                selectedEmployee = item;
                Fullname = item.FullName;
                EmployeeiD = item.EmployeeID;
                Role = item.Role;
                Email = item.Email;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Product");
            }
        }
        public async void OnDoneEditingEmployee()
        {
            selectedEmployee = await App.Employee_Database.GetEmployeeDetsAsync(Convert.ToInt32(itemId));
            {
                selectedEmployee.FullName = Fullname;
                selectedEmployee.EmployeeID = EmployeeiD;
                selectedEmployee.Role = Role;
                selectedEmployee.Email = Email;
            };

            await App.Employee_Database.SaveEmployeeDetsAsync(selectedEmployee);
            await Shell.Current.GoToAsync("..");
        }
    }
}
