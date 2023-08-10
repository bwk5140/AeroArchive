using AeroArchive.Models;
using System;
using System.Numerics;
using Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AeroArchive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void ForgotPasswordTapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://imagikcorp.com/", BrowserLaunchMode.SystemPreferred);
        }

        public bool RememberMe
        {
            get => Preferences.Get(nameof(RememberMe), false);
            set
            {
                Preferences.Set(nameof(RememberMe), value);
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        string username = Preferences.Get(nameof(Username), string.Empty);
        public string Username
        {
            get => username;
            set
            {
                username = value;
                if (RememberMe)
                    Preferences.Set(nameof(Username), value);
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        async void OnLoginClicked(object obj, EventArgs e)
        {
            var isValid = true;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "", "OK");
                return;
            }

            if (string.IsNullOrEmpty(UserNameEntry.Text) || UserNameEntry.Text.Length < 5)
            {
                VisualStateManager.GoToState(UserNameEntry, "Invalid");
                isValid = false;
            }

            else
            {
                VisualStateManager.GoToState(UserNameEntry, "Valid");
            }

            if (string.IsNullOrEmpty(PasswordEntry.Text) || PasswordEntry.Text.Length < 5)
            {
                VisualStateManager.GoToState(PasswordEntry, "Invalid");
                isValid = false;
            }
            else
            {
                VisualStateManager.GoToState(PasswordEntry, "Valid");
            }

            var list = await App.Account_Database.GetRegistrationDetsAsync();

            if (list.Count == 0)
            {
                isValid = false;
                await DisplayAlert("Please create an account", "", "OK");
            }

            else
            {
                if (isValid)
                {
                    foreach (var item in list)
                    {
                        if (UserNameEntry.Text.Equals(item.UserName) && PasswordEntry.Text.Equals(item.Password))
                        {
                            isValid = true;
                            break;
                        }
                        if (!UserNameEntry.Text.Equals(item.UserName))
                        {
                            isValid = false;
                        }
                        if (!PasswordEntry.Text.Equals(item.Password))
                        {
                            isValid = false;
                        }
                    }
                }
            }

            if (!isValid && list.Count > 0 && (!string.IsNullOrEmpty(UserNameEntry.Text)
                && UserNameEntry.Text.Length >= 5) && (!string.IsNullOrEmpty(PasswordEntry.Text)
                && PasswordEntry.Text.Length >= 5))
            { 
                await DisplayAlert("Invalid username or password", "", "OK"); 
            }

            var service = DependencyService.Get<IStatusBar>();
            service?.SetStatusBarColor(isValid ? Color.Green : Color.Red);

            if (isValid)
            {

                try
                {
                    await SecureStorage.SetAsync("UserNametoken", UserNameEntry.Text);
                    await SecureStorage.SetAsync("Passwordtoken", PasswordEntry.Text);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

                 await Clipboard.SetTextAsync("123");
                 await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            InitStates();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            try
            {
                var username = await SecureStorage.GetAsync("UserNametoken");
                var password = await SecureStorage.GetAsync("Passwordtoken");
                UserNameEntry.Text = username;
                PasswordEntry.Text = password;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                LabelConnection.FadeTo(0).ContinueWith((result) => { });
            }
            else
            {
                LabelConnection.FadeTo(1).ContinueWith((result) => { });
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        void InitStates()
        {
            var stateGroup = new VisualStateGroup
            {
                Name = "StrengthStates",
                TargetType = typeof(Label)
            };

            stateGroup.States.Add(CreateState("Blank", "", Color.White));
            stateGroup.States.Add(CreateState("VeryWeak", "\uf023", Color.Red));
            stateGroup.States.Add(CreateState("Weak", "\uf023 \uf023", Color.Orange));
            stateGroup.States.Add(CreateState("Medium", "\uf023 \uf023 \uf023", Color.Yellow));
            stateGroup.States.Add(CreateState("String", "\uf023 \uf023 \uf023 \uf023", Color.Green));
            stateGroup.States.Add(CreateState("VeryStrong", "\uf023 \uf023 \uf023 \uf023 \uf023", Color.Green));

            VisualStateManager.SetVisualStateGroups(this.StrengthIndicator, new VisualStateGroupList { stateGroup });

        }

        void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            var strength = PasswordAdvisor.CheckStrength(e.NewTextValue);
            var strengthName = Enum.GetName(typeof(PasswordScore), strength);
            VisualStateManager.GoToState(this.StrengthIndicator, strengthName);
            VisualStateManager.GoToState(UserNameEntry, "Valid");
            VisualStateManager.GoToState(PasswordEntry, "Valid");
        }

        string strength;

        public string Strength
        {
            get => strength;
            set
            {
                strength = value;
                OnPropertyChanged(nameof(Strength));
            }
        }

        static VisualState CreateState(string strength, string text, Color color)
        {
            var textSetter = new Setter { Value = text, Property = Label.TextProperty };
            var colorSetter = new Setter { Value = color, Property = Label.TextColorProperty };

            return new VisualState
            {
                Name = strength,
                TargetType = typeof(Label),
                Setters = { textSetter, colorSetter }
            };
        }

        private async void RegisterTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}