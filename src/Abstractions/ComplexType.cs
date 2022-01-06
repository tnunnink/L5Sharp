using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Enums;

// For testing purposes
[assembly: InternalsVisibleTo("L5Sharp.Abstractions.Tests")]

namespace L5Sharp.Abstractions
{
    /// <summary>
    /// Represents an abstract implementation of a <see cref="IComplexType"/>. 
    /// </summary>
    public abstract class ComplexType : IComplexType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="members"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected ComplexType(string name, IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Members = members is not null
                ? new ReadOnlyMembers<IMember<IDataType>>(members)
                : new ReadOnlyMembers<IMember<IDataType>>(FindMembers());
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
        /// This method is called by <see cref="Instantiate"/> in order to generate new instances of the <see cref="IDataType"/>.
        /// 
        /// </remarks>
        protected abstract IDataType New(); 

        /// <inheritdoc />
        public IEnumerable<IDataType> GetDependentTypes()
        {
            var set = new HashSet<IDataType>(ComponentNameComparer.Instance);

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
        public override string ToString() => Name;

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