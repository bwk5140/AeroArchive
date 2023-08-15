using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AeroArchive.Utils
{
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public string ShowIcon { get; set; }
        public string HideIcon { get; set; }

        bool hidePassword = true;

        public bool HidePassword 
        {
            get => hidePassword; 
            
            set 
            {
                if (hidePassword != value)
                {
                    hidePassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }        
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = HidePassword? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }
    }
}
