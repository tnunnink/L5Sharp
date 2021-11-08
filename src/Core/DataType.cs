using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : LogixComponent, IUserDefined, IEquatable<DataType>
    {
        private string _name;

        public DataType(string name, string description = null) : base(name, description)
        {
            Validate.DataTypeName(name);
            _name = name;

            Members = new DataTypeMembers(this);
        }

        public DataType(string name, IDataTypeMember<IDataType> dataTypeMember, string description = null)
            : this(name, description)
        {
            Members.Add(dataTypeMember);
        }

        public DataType(string name, IEnumerable<IDataTypeMember<IDataType>> members, string description = null)
            : this(name, description)
        {
            foreach (var member in members)
                Members.Add(member);
        }

        public override string Name => _name;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.User;
        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        //IEnumerable<IMember<IDataType>> IDataType.Members => Members;
        public IDataTypeMembers Members { get; }

        public override void SetName(string name)
        {
            Validate.Name(name);
            Validate.DataTypeName(name);
            _name = name;
        }

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Equals(Members, other.Members)
                   && Equals(Family, other.Family)
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