using System;

namespace L5Sharp.Abstractions
{
    public interface IComponent
    {
        public string Name { get; }
    }

    public interface INotifyNameChanged
    {
        event EventHandler<NameChangedEventArgs> NameChanged;
    }

    public class NameChangedEventArgs : EventArgs
    {
        public NameChangedEventArgs(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
        
        public string OldName { get; }
        public string NewName { get; }
    }
}