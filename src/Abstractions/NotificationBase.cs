using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using L5Sharp.Annotations;

namespace L5Sharp.Abstractions
{
    public class NotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return;

            storage = value;

            RaisePropertyChanged(propertyName);
        }
        
        protected void SetProperty<T>(ref T storage, T value, Action<T> validate,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return;

            validate?.Invoke(value);
            
            storage = value;

            RaisePropertyChanged(propertyName);
        }
        
        protected void SetProperty<T>(ref T storage, T value, Action onChanged,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return;

            storage = value;
            
            onChanged?.Invoke();
            
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
}