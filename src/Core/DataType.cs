using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : LogixComponent, IUserDefined, IEquatable<DataType>
    {
        private string _name;

        public DataType(string name, string description = null) : base (name, description)
        {
            Validate.DataTypeName(name);
            _name = name;

            Members = new DataTypeMembers(this);
        }

        public DataType(string name, IDataTypeMember dataTypeMember, string description = null) 
            : this(name, description)
        {
            Members.Add(dataTypeMember);
        }

        public DataType(string name, IEnumerable<IDataTypeMember> members, string description = null) 
            : this(name, description)
        {
            foreach (var member in members)
                Members.Add(member);
        }

        public override string Name => _name;

        public DataTypeFamily Family => DataTypeFamily.None;

        public DataTypeClass Class => DataTypeClass.User;
        
        public bool IsAtomic => false;

        public TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IDataTypeMembers Members { get; }

        IEnumerable<IMember> IDataType.Members => Members;

        public override void SetName(string name)
        {
            Validate.Name(name);
            Validate.DataTypeName(name);
            SetProperty(ref _name, name, nameof(Name));
        }

        public IEnumerable<IDataType> GetDependentTypes() => GetUniqueMemberTypes(this);
        
        public IEnumerable<IDataType> GetDependentUserTypes() =>
            GetUniqueMemberTypes(this).Where(t => t.Class == DataTypeClass.User);

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

        /// <summary>
        /// Recursively walks the member collections and finds all unique data types of the structure
        /// </summary>
        /// <param name="dataType">The datatype to walk</param>
        /// <returns>An enumeration of all unique data types</returns>
        private static IEnumerable<IDataType> GetUniqueMemberTypes(IDataType dataType)
        {
            var types = new List<IDataType>();

            foreach (var member in dataType.Members)
            {
                types.Add(member.DataType);
                types.AddRange(GetUniqueMemberTypes(member.DataType));
            }

            return types.Distinct();
        }
    }
}