using AeroArchive;
using AeroArchive.Services;
using AeroArchive.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive
{
    public partial class App : Application
    {

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