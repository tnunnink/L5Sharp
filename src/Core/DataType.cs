using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IUserDefined" />
    public class DataType : IUserDefined, IEquatable<DataType>
    {
        /// <summary>
        /// Creates a new instance of a <c>UserDefined</c> data type with the provided arguments.
        /// </summary>
        /// <param name="name">The component name of the type.</param>
        /// <param name="description">the string description of the type.</param>
        /// <param name="members">The collection of members to add to the type.</param>
        public DataType(ComponentName name, string description = null, IEnumerable<IMember<IDataType>> members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Members = new DataTypeMembers(this, members);
        }


        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public Radix Radix => Radix.Null;

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.User;

        /// <inheritdoc />
        public TagDataFormat DataFormat => TagDataFormat.Decorated;

        /// <inheritdoc />
        public IMemberCollection<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IDataType Instantiate()
        {
            return new DataType(Name.Copy(), string.Copy(Description), Members.Select(m => m.Copy()));
        }
        
        /// <summary>
        /// Creates a new <c>UserDefined</c> data type with the provided values.
        /// </summary>
        /// <param name="name">The name of the data type.</param>
        /// <param name="description">the description of the data type.</param>
        /// <param name="members">The collection of members to initialize the data type with.</param>
        /// <returns>A new instance of the <c>UserDefined</c> data type.</returns>
        public static IUserDefined Create(ComponentName name, string description = null,
            IEnumerable<IMember<IDataType>> members = null)
        {
            return new DataType(name, description, members);
        }

        /// <inheritdoc />
        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Members.SequenceEqual(other.Members)
                   && Description == other.Description;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataType)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(DataType left, DataType right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(DataType left, DataType right)
        {
            return !Equals(left, right);
        }
    }
}