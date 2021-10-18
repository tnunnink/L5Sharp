using System.ComponentModel;

namespace L5Sharp.Events
{
    public class PropertyChangedValueEventArgs<T> : PropertyChangedEventArgs
    {
        public PropertyChangedValueEventArgs(string propertyName, T oldValue, T newValue) : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; }
        public T NewValue { get; }
    }
}