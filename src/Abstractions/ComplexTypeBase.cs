using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

// For testing purposes
[assembly: InternalsVisibleTo("L5Sharp.Abstractions.Tests")]

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// Represents an abstract base class for <see cref="IComplexType"/> instances.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All predefined classes and custom user classed can derive from <see cref="ComplexTypeBase"/>.
    /// The user is responsible for specifying the <see cref="Class"/> and implementing the <see cref="New"/> so that
    /// the type can be instantiated by other parts of the code base. This base class will also use reflection to
    /// find field and property members of the the derived class that implement <see cref="IMember{TDataType}"/>
    /// in order to initialize the <see cref="Members"/> collection.
    /// If the user provided members in the base constructor, this will be bypasses.
    /// </para>
    /// </remarks>
    public abstract class ComplexTypeBase : IComplexType
    {
        /// <summary>
        /// Creates a new <see cref="ComplexTypeBase"/> instance with the provided name and optional members.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="members">An optional collection of member to initialize the type with.</param>
        /// <exception cref="ArgumentNullException">name is null.</exception>
        /// <remarks>
        /// This constructor will attempt to find field/property members of the derived type using reflection in order
        /// to initialize it's member collection. This is done so that the derived classed do not need to explicitly
        /// provided the collection of members. Note that if the member properties not initialized when the base
        /// constructor is called, the null references..
        /// </remarks>
        protected ComplexTypeBase(string name, IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Members = ValidateMembers(members ?? FindMembers());
        }

        /// <inheritdoc />
        public virtual string Name { get; }

        /// <inheritdoc />
        public virtual string Description => string.Empty;

        /// <inheritdoc />
        public virtual DataTypeFamily Family => DataTypeFamily.None;

        /// <inheritdoc />
        public abstract DataTypeClass Class { get; }

        /// <inheritdoc />
        public IEnumerable<IMember<IDataType>> Members { get; }

        /// <inheritdoc />
        public IDataType Instantiate() => New();

        /// <summary>
        /// Creates a new instance of the <see cref="IComplexType"/> with default values.
        /// </summary>
        /// <returns>A new <see cref="IDataType"/> instance with default values.</returns>
        /// <remarks>
        /// This method is called by <see cref="Instantiate"/> in order to generate new instances
        /// of the <see cref="IDataType"/>.
        /// </remarks>
        protected abstract IDataType New();

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <summary>
        /// Validates the provided member collection by ensuring they are not null and member names are unique. 
        /// </summary>
        /// <param name="members">The member collection to validate.</param>
        /// <returns>A new collection of members that have been validated.</returns>
        /// <exception cref="ArgumentNullException">members contains a null instance.</exception>
        /// <exception cref="ComponentNameCollisionException">A duplicate member name exists in members.</exception>
        private static IEnumerable<IMember<IDataType>> ValidateMembers(IEnumerable<IMember<IDataType>> members)
        {
            var valid = new Dictionary<string, IMember<IDataType>>();

            foreach (var member in members)
            {
                if (member is null)
                    throw new ArgumentNullException(nameof(members), "Can not register null members.");

                if (valid.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, member.GetType());

                valid.Add(member.Name, member);
            }

            return valid.Values.AsEnumerable();
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
    }
}