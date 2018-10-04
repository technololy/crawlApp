using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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

    }
}
