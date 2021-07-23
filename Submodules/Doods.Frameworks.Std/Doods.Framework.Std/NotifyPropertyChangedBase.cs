using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Doods.Framework.Std
{
    /// <summary>
    ///     NotifyPropertyChangedBase object with INotifyPropertyChanged implemented
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        ///     Sets the property.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <param name="name"></param>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        protected bool SetProperty<TProperty>(ref TProperty currentValue, TProperty newValue,
            [CallerMemberName] string name = "")
        {
            return SetProperty(ref currentValue, newValue, null, null, name);
        }


        /// <summary>
        ///     Sets the property.
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <param name="name"></param>
        /// <returns><c>true</c>, if property was set, <c>false</c> otherwise.</returns>
        protected bool SetProperty<TProperty>(ref TProperty currentValue, TProperty newValue
            , Action onChanged,
            Func<TProperty, TProperty, bool> validateValue, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<TProperty>.Default.Equals(currentValue, newValue))
                return false;

            if (validateValue != null && !validateValue(currentValue, newValue))
                return false;
            currentValue = newValue;
            onChanged?.Invoke();
            OnPropertyChanged(name);
            return true;
        }
    }
}