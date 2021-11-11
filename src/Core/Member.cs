using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public class Member<TDataType> :
        IMember<TDataType>,
        IEquatable<Member<TDataType>>,
        IPrototype<IMember<TDataType>>
        where TDataType : IDataType
    {
        //this is a work around to accomodate how descriptions are propagated in RSLogix
        private string _parentDescription;
        private string _overridenDescription;
        private readonly string _defaultDescription;

        public Member(string name, TDataType dataType, Dimensions dimensions, Radix radix,
            ExternalAccess externalAccess, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            DataType = dataType;
            Dimensions = dimensions ?? Dimensions.Empty;
            if (Dimensions.AreMultiDimensional)
                throw new ArgumentException("Member can only have single dimensional arrays");
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            ExternalAccess = externalAccess != null ? externalAccess : ExternalAccess.ReadWrite;
            _defaultDescription = description;
            Elements = InstantiateElements();
        }

        public string Name { get; }
        public string Description => GetDescription();
        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; }
        public IMember<TDataType>[] Elements { get; }

        public void SetRadix(Radix radix)
        {
            if (radix == null)
                throw new ArgumentNullException(nameof(radix));

            if (!(DataType is IAtomic atomic))
                throw new ComponentNotConfigurableException(nameof(Radix), GetType(),
                    "Radix can only be set on atomic members");

            atomic.SetRadix(radix);

            if (Elements.Length == 0) return;

            foreach (var element in Elements)
                if (element.DataType is IAtomic atomicType)
                    atomicType.SetRadix(radix);
        }

        internal void SetParentDescription(string description)
        {
            _parentDescription = description;
        }

        public void SetDescription(string description)
        {
            _overridenDescription = description;
        }

        public IMember<TDataType> Copy()
        {
            var dataType = (TDataType)DataType.Instantiate();
            return Member.OfType(Name, dataType, Dimensions.Copy(), Radix, ExternalAccess, Description);
        }

        public bool Equals(Member<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimensions == other.Dimensions &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description && Elements.SequenceEqual(other.Elements);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member<TDataType>)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimensions, Radix, ExternalAccess, Description, Elements);
        }

        public static bool operator ==(Member<TDataType> left, Member<TDataType> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member<TDataType> left, Member<TDataType> right)
        {
            return !Equals(left, right);
        }

        private IMember<TDataType>[] InstantiateElements()
        {
            var elements = new List<IMember<TDataType>>(Dimensions);

            for (var i = 0; i < Dimensions; i++)
                elements.Add(new Member<TDataType>($"[{i}]", (TDataType)DataType.Instantiate(),
                    Dimensions.Empty, Radix, ExternalAccess, Description));

            return elements.ToArray();
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

    public static class Member
    {
        public static IMember<IDataType> New(string name, IDataType dataType,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IMember<IDataType> New(string name, IDataType dataType, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name, TDataType dataType,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new Member<TDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name, TDataType dataType, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType
        {
            return new Member<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Member<TDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name, Dimensions dimension,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Member<TDataType>(name, dataType, dimension, radix, externalAccess, description);
        }
    }
}