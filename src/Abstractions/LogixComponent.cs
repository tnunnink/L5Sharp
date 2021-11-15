using System;
using L5Sharp.Core;

namespace L5Sharp.Abstractions
{
    public abstract class LogixComponent : ILogixComponent, IEquatable<LogixComponent>
    {
        protected LogixComponent(ComponentName name, string description)
        {
            Name = name;
            Description = description;
        }


        public ComponentName Name { get; private set; }
        public string Description { get; private set; }

        public void SetName(ComponentName name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name == Name) return;
            Name = name;
        }

        public virtual void SetDescription(string description)
        {
            Description = description;
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