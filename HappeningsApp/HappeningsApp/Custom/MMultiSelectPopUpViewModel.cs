using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HappeningsApp.Custom
{
    public class MMultiSelectPopUpViewModel : INotifyPropertyChanged
    {
        private bool _selectedSwitch;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool SelectedSwitch
        {
            get => _selectedSwitch;
            set
            {
                _selectedSwitch = value;
                OnPropertyChanged();
            }
        }


        public Command DoneCommand
        {
            get
            {
              return new Command(()=>DoDone())  ;
            }
            
        }

        private void DoDone()
        {
            string a = "ff";
        }
    }
}
