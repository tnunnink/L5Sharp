using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public abstract class ComplexType : IComplexType, IEquatable<ComplexType>
    {
        protected internal ComplexType(string name, string? description = null, 
            IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Members = members ?? FindMemberFields();
        }
        
        protected ComplexType(ComponentName name, string? description = null, 
            IEnumerable<IMember<IDataType>>? members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            Members = members ?? FindMemberFields();
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
        public IDataType Instantiate()
        {
            return New();
        }

        /// <summary>
        /// Creates new instance of the current type with default values.
        /// </summary>
        /// <remarks>
        /// Called when <see cref="Instantiate"/> is called. This abstraction is here to let the
        /// base class define the code for instantiating a new version of itself. Simply return <c>new  MyPredefined()</c>.
        /// </remarks>
        /// <returns>A new instance of the current type with default values.</returns>
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
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Members);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Indicates whether one object is equal to another object of the same type.
        /// </summary>
        /// <param name="left">An instance of the object to compare.</param>
        /// <param name="right">An instance of the object to compare.</param>
        /// <returns>ture if the objects are equal, otherwise false.</returns>
        public static bool operator ==(ComplexType left, ComplexType right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Indicates whether one object is not equal to another object of the same type.
        /// </summary>
        /// <param name="left">An instance of the object to compare.</param>
        /// <param name="right">An instance of the object to compare.</param>
        /// <returns>ture if the objects are not equal, otherwise false.</returns>
        public static bool operator !=(ComplexType left, ComplexType right)
        {
            return !Equals(left, right);
        }
        
        /// <summary>
        /// Adds all instance fields of type <see cref="IMember{TDataType}"/> to the <see cref="Members"/> collection
        /// using reflection so that the developer does not need to register each member individually.
        /// </summary>
        private IEnumerable<IMember<IDataType>> FindMemberFields()
        {
            var fields = GetType().GetFields().Where(f =>
                f.FieldType.IsGenericType &&
                f.FieldType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IMember<>))).ToList();

            return fields.Select(f => (IMember<IDataType>)f.GetValue(this));
        }
    }
}