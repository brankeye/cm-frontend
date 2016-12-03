using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace cm.frontend.core.Domain.Extensions.NotifyPropertyChanged
{
    public static class Core
    {
        public static bool SetProperty<T>(
            this INotifyPropertyChanged notifier,
            ref T backingStore,
            T value,
            PropertyChangedEventHandler handler,
            bool alwaysInvoke = false,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (!alwaysInvoke)
            {
                if (EqualityComparer<T>.Default.Equals(backingStore, value))
                    return false;
            }
            
            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(notifier, handler, propertyName);

            return true;
        }

        public static void OnPropertyChanged(this INotifyPropertyChanged notifier, PropertyChangedEventHandler propertyChanged, [CallerMemberName] string propertyName = @"")
        {
            propertyChanged?.Invoke(notifier, new PropertyChangedEventArgs(propertyName));
        }
    }
}
