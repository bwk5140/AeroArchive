using AeroArchive.AppDatabase;
using AeroArchive.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace AeroArchive
{
    public partial class App : Application
    {
        static RegistrationDatabase AccountDatabase;

        public static RegistrationDatabase Account_Database
        {
            get
            {
                if (AccountDatabase == null)
                {
                    AccountDatabase = new RegistrationDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Registration.db3"));
                }
                return AccountDatabase;
            }
        }

        static ProductDatabase ProdDatabase;

        public static ProductDatabase Prod_Database
        {
            get
            {
                if (ProdDatabase == null)
                {
                    ProdDatabase = new ProductDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Products.db3"));
                }
                return ProdDatabase;
            }
        }

        static EmployeeDatabase EmployeeDatabase;

        public static EmployeeDatabase Employee_Database
        {
            get
            {
                if (EmployeeDatabase == null)
                {
                    EmployeeDatabase = new EmployeeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employees.db3"));
                }
                return EmployeeDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}