using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class DataType : LogixComponent, IUserDefined, IEquatable<DataType>
    {
        public DataType(string name, string description = null, IEnumerable<IMember<IDataType>> members = null)
            : base(name, description)
        {
            Members = members == null ? new Members(this) : new Members(this, members);
        }
        
        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.User;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IMembers Members { get; }

        public IDataType Instantiate()
        {
            var members = new Members(this);

            foreach (var member in Members)
            {
                var copy = Member.Create<IDataType>(member.Name, member.DataType, member.Dimension, member.Radix,
                    member.ExternalAccess, member.Description);
                members.Add(copy);
            }
            
            return new DataType(Name, Description, members);
        }

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Members.SequenceEqual(other.Members)
                   && Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataType)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(DataType left, DataType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataType left, DataType right)
        {
            return !Equals(left, right);
        }
    }
}