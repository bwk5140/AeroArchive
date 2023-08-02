using AeroArchive;
using AeroArchive.RegistrationDB;
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
        static RegistrationDatabase database;

        public static RegistrationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RegistrationDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Registration.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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