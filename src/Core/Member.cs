using System;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Member : IMember, IEquatable<Member>
    {
        internal Member(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            DataType = dataType ?? Logix.DataType.Undefined;
            Dimensions = dimension ?? Dimensions.Empty;
            Radix = !(DataType is IAtomic atomic) ? Radix.Null 
                : radix == null ? atomic.DefaultRadix : radix;
            ExternalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
            Description = description;
        }

        public string Name { get; }
        public IDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public string Description { get; }

        public bool Equals(Member other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimensions == other.Dimensions &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimensions, Radix, ExternalAccess, Description);
        }

        public static bool operator ==(Member left, Member right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !Equals(left, right);
        }
    }
}