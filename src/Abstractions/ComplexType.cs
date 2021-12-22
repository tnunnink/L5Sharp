using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

// For testing purposes
[assembly: InternalsVisibleTo("L5Sharp.Abstractions.Tests")]

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// Represents an abstract implementation of a <see cref="IComplexType"/>. 
    /// </summary>
    public abstract class ComplexType : IComplexType, IEquatable<ComplexType>
    {
        /// <summary>
        /// Creates a new instance of a <c>ComplexType</c> with the provided arguments.
        /// </summary>
        /// <remarks>
        /// This constructor is internal to prevent it from being inherited outside the assembly.
        /// Users will inherit from either UserDefined or some other public types to create strongly typed objects.
        /// </remarks>
        /// <param name="name">The name of the type.</param>
        /// <param name="description">The description of the the type.</param>
        /// <param name="members">A collection of members to initialize the type with.</param>
        /// <exception cref="ArgumentNullException">When name is null.</exception>
        /// <exception cref="ComponentNameCollisionException">If a duplicate member name is encountered.</exception>
        /// <exception cref="CircularReferenceException">If a member type references the current data type.</exception>
        internal ComplexType(string name, string? description = null,
            IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Members = members is not null
                ? new MemberCollection<IMember<IDataType>>(members)
                : new MemberCollection<IMember<IDataType>>(FindMembers());
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public virtual DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public abstract DataTypeClass Class { get; }

        /// <inheritdoc />
        public virtual IMemberCollection<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IDataType Instantiate() => New();

        /// <summary>
        /// Creates new instance of the current type with default values.
        /// </summary>
        /// <remarks>
        /// Called when <see cref="Instantiate"/> is called. This abstraction is here to let the
        /// base class define the code for instantiating a new version of itself.
        /// </remarks>
        /// <returns>A new instance of the current <c>DataType</c> with default values.</returns>
        protected abstract IDataType New();

        /// <inheritdoc />
        public bool Equals(ComplexType? other)
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
            return obj.GetType() == GetType() && Equals((ComplexType)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(Name, Description, Members);

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">An instance of the object to compare.</param>
        /// <param name="right">An instance of the object to compare.</param>
        /// <returns>ture if the objects are equal, otherwise false.</returns>
        public static bool operator ==(ComplexType left, ComplexType right) => Equals(left, right);

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">An instance of the object to compare.</param>
        /// <param name="right">An instance of the object to compare.</param>
        /// <returns>ture if the objects are not equal, otherwise false.</returns>
        public static bool operator !=(ComplexType left, ComplexType right) => !Equals(left, right);
        

        /// <summary>
        /// Gets all <c>IMember</c> properties and fields defined for the current <c>IComplexType</c> using reflection.
        /// </summary>
        /// <remarks>
        /// This is a helper to allow predefined data type members to be registered to the <see cref="Members"/>
        /// collection without having to explicitly adding them via constructor.
        /// </remarks>
        /// <returns>
        /// A collection of all <c>IMember</c> objects defined as fields or properties for the current instance.
        /// </returns>
        private IEnumerable<IMember<IDataType>> FindMembers()
        {
            var members = new List<IMember<IDataType>>();

            members.AddRange(FindMemberFields());
            members.AddRange(FindMemberProperties());

            return members;
        }

        /// <summary>
        /// Gets all <c>IMember</c> fields of the current <c>IComplexType</c> using reflection.
        /// </summary>
        /// <remarks>
        /// This is a helper to allow predefined data type members to be registered to the <see cref="Members"/>
        /// collection without having to explicitly adding them via constructor.
        /// </remarks>
        /// <returns>A collection of all <c>IMember</c> objects defined as field for the current instance.</returns>
        private IEnumerable<IMember<IDataType>> FindMemberFields()
        {
            var fields = GetType().GetFields().Where(f =>
                f.FieldType.IsGenericType &&
                f.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            return fields.Select(f => (IMember<IDataType>)f.GetValue(this));
        }

        /// <summary>
        /// Gets all <c>IMember</c> properties of the current <c>IComplexType</c> using reflection.
        /// </summary>
        /// <remarks>
        /// This is a helper to allow predefined data type members to be registered to the <see cref="Members"/>
        /// collection without having to explicitly adding them via constructor.
        /// </remarks>
        /// <returns>A collection of all <c>IMember</c> objects defined as properties for the current instance.</returns>
        private IEnumerable<IMember<IDataType>> FindMemberProperties()
        {
            var properties = GetType().GetProperties().Where(p =>
                p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            return properties.Select(f => (IMember<IDataType>)f.GetValue(this));
        }
    }
}