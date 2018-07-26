using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace DienMay.ViewModel.Base
{
    public class BaseNotifyPropertyChange : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, Action action, string propertyName = null)
        {
            var r = SetProperty(ref storage, value, propertyName);
            if (r) action?.Invoke();
            return r;
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
