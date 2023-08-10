using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AeroArchive.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Home";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://bulletins.psu.edu/undergraduate/colleges/behrend/software-engineering-bs/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}