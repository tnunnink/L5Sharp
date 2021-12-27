using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Common;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="L5Sharp.IUserDefined" />
    public class UserDefined : IUserDefined, IEquatable<UserDefined>
    {
        /// <summary>
        /// Creates a new instance of a <c>UserDefined</c> data type with the provided arguments.
        /// </summary>
        /// <remarks>
        /// To create an instance of a <c>IUserDefined</c> use <see cref="Create"/>.
        /// </remarks>
        /// <param name="name">The component name of the type.</param>
        /// <param name="description">the string description of the type.</param>
        /// <param name="members">The collection of members to add to the type.</param>
        internal UserDefined(string name, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Members = new Members<IMember<IDataType>>(this, members);
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public DataTypeClass Class => DataTypeClass.User;

        /// <inheritdoc />
        public IMembers<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IEnumerable<IDataType> GetDependentTypes()
        {
            var set = new HashSet<IDataType>(new ComponentNameComparer<IDataType>());

            foreach (var member in Members)
            {
                set.Add(member.DataType);

                if (member.DataType is not IComplexType complex) continue;

                foreach (var dataType in complex.GetDependentTypes())
                    set.Add(dataType);
            }

            return set;
        }

        /// <inheritdoc />
        public IDataType Instantiate() =>
            new UserDefined(string.Copy(Name), string.Copy(Description), Members.Select(m => m.Copy()));

        /// <summary>
        /// Creates a new <c>UserDefined</c> data type with the provided values.
        /// </summary>
        /// <param name="name">The name of the data type.</param>
        /// <param name="description">the description of the data type.</param>
        /// <param name="members">The collection of members to initialize the data type with.</param>
        /// <returns>A new instance of the <c>UserDefined</c> data type.</returns>
        public static IUserDefined Create(ComponentName name, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            return new UserDefined(name, description, members);
        }


        /// <inheritdoc />
        public bool Equals(UserDefined? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name
                   && Description == other.Description
                   && Members.SequenceEqual(other.Members);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((UserDefined)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Name, Description, Members);

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public static bool operator ==(UserDefined left, UserDefined right) => Equals(left, right);

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">The left instance of the object.</param>
        /// <param name="right">The right instance of the object.</param>
        /// <returns>True if the two objects are not equal, otherwise false.</returns>
        public static bool operator !=(UserDefined left, UserDefined right) => !Equals(left, right);

        IReadOnlyMembers<IMember<IDataType>> IComplexType.Members => Members;
    }
}