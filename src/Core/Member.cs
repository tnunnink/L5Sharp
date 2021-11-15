using System;
using System.Xml.Serialization;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    /// <inheritdoc cref="IMember{TDataType}" />
    public class Member<TDataType> : IMember<TDataType>, IEquatable<Member<TDataType>>
        where TDataType : IDataType
    {
        //this is a work around to accomodate how descriptions are propagated in RSLogix
        private string _parentDescription;
        private string _overridenDescription;
        private readonly string _defaultDescription;

        internal Member(ComponentName name, TDataType dataType, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType;
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            ExternalAccess = externalAccess != null ? externalAccess : ExternalAccess.ReadWrite;
            _defaultDescription = description;
        }

        /// <inheritdoc />
        public ComponentName Name { get; }

        /// <inheritdoc />
        public string Description => GetDescription();

        /// <inheritdoc />
        public TDataType DataType { get; }
        
        /// <inheritdoc />
        [XmlAttribute("Dimension")] //overriding naming for the serialization since data type members are single 'Dimension'
        public Dimensions Dimensions => Dimensions.Empty;

        /// <inheritdoc />
        public Radix Radix => DataType.Radix;

        /// <inheritdoc />
        public ExternalAccess ExternalAccess { get; }

        /// <inheritdoc />
        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(DataType is IAtomic atomic))
                throw new ComponentNotConfigurableException(nameof(Radix), GetType(),
                    "Radix can only be set on atomic members");

            atomic.SetRadix(radix);
        }

        internal void SetParentDescription(string description)
        {
            _parentDescription = description;
        }

        /// <inheritdoc />
        public void SetDescription(string description)
        {
            _overridenDescription = description;
        }

        /// <inheritdoc />
        public bool Equals(Member<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimensions == other.Dimensions &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member<TDataType>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimensions, Radix, ExternalAccess, Description);
        }
        
        /// <summary>
        /// Determines whether two objects are equal. 
        /// </summary>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns>True if the left object is equal to the right object. Otherwise, False</returns>
        public static bool operator ==(Member<TDataType> left, Member<TDataType> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two objects are not equal. 
        /// </summary>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns>True if the left object is not equal to the right object. Otherwise, False</returns>
        public static bool operator !=(Member<TDataType> left, Member<TDataType> right)
        {
            return !Equals(left, right);
        }

        private string GetDescription()
        {
            if (!string.IsNullOrEmpty(_overridenDescription))
                return _overridenDescription;

            if (!string.IsNullOrEmpty(_parentDescription) && !string.IsNullOrEmpty(_defaultDescription))
                return $"{_parentDescription} {_defaultDescription}";

            return !string.IsNullOrEmpty(_parentDescription) ? _parentDescription : _defaultDescription;
        }
    }

    /// <summary>
    /// Static factory class that creates instances of <see cref="IMember{TDataType}"/> and <see cref="IArrayMember{TDataType}"/> 
    /// </summary>
    public static class Member
    {
        /// <summary>
        /// Creates an instance of <see cref="IMember{IDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="dataType">The <see cref="IDataType"/> of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <returns>A new instance of <see cref="IMember{IDataType}"/></returns>
        public static IMember<IDataType> Create(ComponentName name, IDataType dataType,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a strongly typed instance of <see cref="IMember{TDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.
        /// TDataType must implement IDataType and have parameterless constructor</typeparam>
        /// <returns>A new instance of <see cref="IMember{TDataType}"/></returns>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            return new Member<TDataType>(name, new TDataType(), radix, externalAccess, description);
        }

        /// <summary>
        /// Creates a strongly typed instance of <see cref="IMember{TDataType}"/> with the specified arguments.
        /// </summary>
        /// <param name="name">The <see cref="ComponentName"/> value of the member.</param>
        /// <param name="dataType">The <see cref="TDataType"/> value of the member.</param>
        /// <param name="radix">The <see cref="Radix"/> option of the member.</param>
        /// <param name="externalAccess">The <see cref="ExternalAccess"/> option of the member.</param>
        /// <param name="description">The member description value.</param>
        /// <typeparam name="TDataType">The <see cref="IDataType"/> of the member.
        /// TDataType must implement IDataType and have parameterless constructor</typeparam>
        /// <returns>A new instance of <see cref="IMember{TDataType}"/></returns>
        public static IMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new Member<TDataType>(name, dataType, radix, externalAccess, description);
        }

        public static IArrayMember<IDataType> Create(ComponentName name, IDataType dataType, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new ArrayMember<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            return new ArrayMember<TDataType>(name, new TDataType(), dimensions, radix, externalAccess, description);
        }

        public static IArrayMember<TDataType> Create<TDataType>(ComponentName name, TDataType dataType,
            Dimensions dimensions, Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new ArrayMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}