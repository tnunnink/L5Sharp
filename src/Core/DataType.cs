using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataType : LogixComponent, IUserDefined, IEquatable<DataType>
    {
        private string _name;
        private readonly List<IDataTypeMember> _members = new List<IDataTypeMember>();

        public DataType(string name, string description = null) : base (name, description)
        {
            Validate.DataTypeName(name);
            _name = name;
        }

        public DataType(string name, DataTypeMember dataTypeMember, string description = null) 
            : this(name, description)
        {
            AddMemberComponent(dataTypeMember);
        }

        public DataType(string name, IEnumerable<DataTypeMember> members, string description = null) 
            : this(name, description)
        {
            foreach (var member in members)
                AddMemberComponent(member);
        }

        public override string Name => _name;

        public DataTypeFamily Family => DataTypeFamily.None;

        public DataTypeClass Class => DataTypeClass.User;
        
        public bool IsAtomic => false;

        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        public IEnumerable<IMember> Members => _members.AsEnumerable();

        public override void SetName(string name)
        {
            Validate.Name(name);
            Validate.DataTypeName(name);
            SetProperty(ref _name, name, nameof(Name));
        }

        public IDataTypeMember GetMember(string name) => GetMemberByName(name);

        public IEnumerable<IDataType> GetDependentTypes() => GetUniqueMemberTypes(this);
        
        public IEnumerable<IDataType> GetDependentUserTypes() =>
            GetUniqueMemberTypes(this).Where(t => t.Class == DataTypeClass.User);

        public void AddMember(string name, IDataType dataType, string description = null,
            Dimensions dimension = null, Radix radix = null, ExternalAccess access = null) =>
            AddMemberComponent(new DataTypeMember(name, dataType, dimension, radix, access, description));

        public void RemoveMember(string name) => RemoveMemberComponent((DataTypeMember)GetMemberByName(name));

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
        /// Determines if the members collection contains a member with the provided name
        /// </summary>
        /// <param name="name">The name of the member to find</param>
        /// <returns>True when a member with the provided name exists</returns>
        private bool HasMemberName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Name can not be null");
            
            return _members.Any(m => m.Name == name);
        }

        /// <summary>
        /// Gets a single member by name from the type's member collection
        /// </summary>
        /// <param name="name">The name of the member to get</param>
        /// <returns></returns>
        private IDataTypeMember GetMemberByName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "Name can not be null");
            
            return _members.SingleOrDefault(m => m.Name == name);
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

        /// <summary>
        /// Adds a member to the data type member collection
        /// </summary>
        /// <param name="dataTypeMember">The member to add</param>
        /// <exception cref="ArgumentNullException">Thrown when member is null</exception>
        private void AddMemberComponent(DataTypeMember dataTypeMember)
        {
            if (dataTypeMember == null)
                throw new ArgumentNullException(nameof(dataTypeMember), "Member can not be null");

            if (HasMemberName(dataTypeMember.Name))
                Throw.ComponentNameCollisionException(dataTypeMember.Name, typeof(DataTypeMember));

            if (dataTypeMember.DataType.Equals(this))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{dataTypeMember.DataType.Name}'");
            
            dataTypeMember.PropertyChanged += OnMemberPropertyChanged;
            
            _members.Add(dataTypeMember);
            
            RaisePropertyChanged(nameof(Members));
        }

        /// <summary>
        /// Adds a member to the data type member collection
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dataTypeMember">The member to add</param>
        /// <exception cref="ArgumentNullException">Thrown when member is null</exception>
        private void InsertMemberComponent(int index, DataTypeMember dataTypeMember)
        {
            if (dataTypeMember == null)
                throw new ArgumentNullException(nameof(dataTypeMember), "Member can not be null");

            if (HasMemberName(dataTypeMember.Name))
                Throw.ComponentNameCollisionException(dataTypeMember.Name, typeof(DataTypeMember));

            if (dataTypeMember.DataType.Equals(this))
                throw new CircularReferenceException(
                    $"Member can not be same type as parent type '{dataTypeMember.DataType.Name}'");
            
            dataTypeMember.PropertyChanged += OnMemberPropertyChanged;
            
            _members.Insert(index, dataTypeMember);
            
            RaisePropertyChanged(nameof(Members));
        }

        /// <summary>
        /// Removes the member from the data type's member collection.
        /// </summary>
        /// <param name="dataTypeMember"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void RemoveMemberComponent(DataTypeMember dataTypeMember)
        {
            if (dataTypeMember == null) return;

            if (!HasMemberName(dataTypeMember.Name)) return;

            dataTypeMember.PropertyChanged -= OnMemberPropertyChanged;
            
            _members.Remove(dataTypeMember);
            
            RaisePropertyChanged(nameof(Members));
        }

        /// <summary>
        /// Handles Member property changed event by raising 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemberPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(Members));
        }
    }
}