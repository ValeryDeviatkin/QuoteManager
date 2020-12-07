using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wpf.Tools.Base
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public bool SetProperty<T>(
            ref T source,
            T newValue,
            Action onChanged = null,
            Action onChanging = null,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(source, newValue))
            {
                return false;
            }

            var shouldNotify = !string.IsNullOrWhiteSpace(propertyName);

            if (shouldNotify)
            {
                OnPropertyChanging(propertyName, onChanging);
            }

            source = newValue;

            if (shouldNotify)
            {
                OnPropertyChanged(propertyName, onChanged);
            }

            return true;
        }

        private void OnPropertyChanging(string propertyName, Action action)
        {
            action?.Invoke();

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName, Action action)
        {
            action?.Invoke();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}