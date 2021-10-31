using System;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class LogixComponent : IMutableLogixComponent, IEquatable<LogixComponent>
    {
        private string _name;
        private string _description;
        
        protected LogixComponent(string name, string description)
        {
            Validate.Name(name);
            
            _name = name;
            _description = description;
        }

        public virtual string Name => _name;

        public virtual string Description => _description;

        public virtual void SetName(string name)
        {
            Validate.Name(name);
            _name = name;
        }

        public virtual void SetDescription(string description)
        {
            _description = description;
        }

        public bool Equals(LogixComponent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((LogixComponent)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

        public static bool operator ==(LogixComponent left, LogixComponent right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LogixComponent left, LogixComponent right)
        {
            return !Equals(left, right);
        }
    }
}