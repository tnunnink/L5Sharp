using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class ReadOnlyMember : IMember, IEquatable<ReadOnlyMember>
    {
        internal ReadOnlyMember(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
        {
            Name = name;
            DataType = dataType ?? Predefined.Undefined;
            Dimension = dimension;
            Radix = DataType.Equals(Predefined.Undefined) ? Radix.Null : radix == null ? DataType.DefaultRadix : radix;
            ExternalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
            Description = description;
        }

        internal ReadOnlyMember(IMember member) : this(member.Name, member.DataType, member.Dimension, member.Radix,
            member.ExternalAccess, member.Description){}

        public string Name { get; }
        public IDataType DataType { get; }
        public ushort Dimension { get; }
        public Radix Radix { get; }
        public ExternalAccess ExternalAccess { get; }
        public string Description { get; }

        public static ReadOnlyMember New(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Name can not be null");
            Validate.Name(name);

            return new ReadOnlyMember(name, dataType, dimension, radix, externalAccess, description);
        }
        
        public bool Equals(ReadOnlyMember other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimension == other.Dimension &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ReadOnlyMember)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimension, Radix, ExternalAccess, Description);
        }

        public static bool operator ==(ReadOnlyMember left, ReadOnlyMember right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ReadOnlyMember left, ReadOnlyMember right)
        {
            return !Equals(left, right);
        }
    }
}