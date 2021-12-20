using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;

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

            members ??= FindMembers();
            Members = ValidateMembers(members);
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
        public virtual IEnumerable<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IMember<IDataType>? GetMember(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must be not be null or empty.");

            return GetMemberInternal(this, name);
        }

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> GetMembers()
        {
            var members = new List<IMember<IDataType>>();

            foreach (var member in Members)
            {
                members.Add(member);

                if (member.DataType is IComplexType complex)
                    members.AddRange(complex.GetMembers());
            }

            return members;
        }

        /// <inheritdoc />
        public IEnumerable<string> GetMemberNames()
        {
            var names = new List<string>();

            foreach (var member in Members)
            {
                names.Add(member.Name);

                if (member.DataType is IComplexType complexType)
                    names.AddRange(complexType.GetMemberNames());
            }

            return names;
        }

        /// <inheritdoc />
        public IEnumerable<IDataType> GetDependentTypes()
        {
            var types = new List<IDataType>();

            foreach (var member in Members)
            {
                types.Add(member.DataType);

                if (member.DataType is IComplexType complexType)
                    types.AddRange(complexType.GetDependentTypes());
            }

            return types.Distinct();
        }

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
        /// Validates the provided set of members to ensure no duplicate names or circular type references are found.
        /// </summary>
        /// <param name="members">A collection of <c>IMember</c> instance to validate.</param>
        /// <returns>The provided collection of valid members.</returns>
        /// <exception cref="ComponentNameCollisionException">If a duplicate name is encountered.</exception>
        /// <exception cref="CircularReferenceException">If a member of the provided collection references the current data type.</exception>
        private IEnumerable<IMember<IDataType>> ValidateMembers(IEnumerable<IMember<IDataType>> members)
        {
            var registry = new Dictionary<string, IMember<IDataType>>();

            foreach (var member in members)
            {
                if (member is null)
                    throw new ArgumentNullException(nameof(member), "Member must be initialized before validation");

                if (registry.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, member.GetType());

                if (member.DataType.Equals(this))
                    throw new CircularReferenceException(this);

                registry.Add(member.Name, member);
            }

            return registry.Values;
        }

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

        /// <summary>
        /// Gets an <c>IMember</c> with the provided name from the current <c>IComplexType</c>
        /// </summary>
        /// <param name="type">The root <c>IComplexType</c> to start traversal of members.</param>
        /// <param name="name">The full member path to the desired <c>IMember</c> instance.</param>
        /// <returns>
        /// If the member name is found in either immediate or nested members, then a <c>IMember</c>
        /// instance with the specified name; otherwise, null.
        /// </returns>
        private static IMember<IDataType>? GetMemberInternal(IComplexType? type, string name)
        {
            IMember<IDataType>? member = default;

            // Get the last member name of the input name. This is the member we are targeting.
            // If we don't end up with it we want to return null.
            var target = name.Split('.').Last();

            while (type is not null && !name.IsEmpty())
            {
                // This gets and removes the first member name of the string name
                name = name.ConsumeWhile(c => c != '.', out var memberName).TrimStart('.');

                // Get the member by name for the current complex type.
                member = type.Members.SingleOrDefault(x => x.Name == memberName);

                // Set the next type for iteration.
                // If the member is null or the member data type is not complex, we will jump out of the loop.
                type = member?.DataType as IComplexType;
            }

            return member?.Name == target ? member : null;
        }
    }
}