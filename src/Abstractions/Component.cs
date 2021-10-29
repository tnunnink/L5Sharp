using System;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class Component : NotificationBase, IComponent, IEquatable<Component>
    {
        protected Component(string name, string description)
        {
            Validate.Name(name);
            
            Name = name;
            Description = description;
        }
        
        public virtual string Name { get; }

        public virtual string Description { get; }

        public bool Equals(Component other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Component)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

        public static bool operator ==(Component left, Component right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Component left, Component right)
        {
            return !Equals(left, right);
        }
    }
}