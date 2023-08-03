using AeroArchive;
using AeroArchive.AppDatabase;
using AeroArchive.Services;
using AeroArchive.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive
{
    public partial class App : Application
    {
        static RegistrationDatabase AccountDatabase;

        public static RegistrationDatabase EmployeeDatabase
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
                    ProdDatabase = new ProductDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Registration.db2"));
                }
                return ProdDatabase;
            }
        }

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
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