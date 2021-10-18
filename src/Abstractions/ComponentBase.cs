using System;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class ComponentBase : NotificationBase, IComponent, IEquatable<ComponentBase>
    {
        private string _name;
        private string _description;

        protected ComponentBase(string name, string description)
        {
            Validate.Name(name);
            
            _name = name;
            _description = description;
        }
        
        public virtual string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, Validate.Name);
        }

        public virtual string Description   
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool Equals(ComponentBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name && _description == other._description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ComponentBase)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_name, _description);
        }

        public static bool operator ==(ComponentBase left, ComponentBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ComponentBase left, ComponentBase right)
        {
            return !Equals(left, right);
        }
    }
}