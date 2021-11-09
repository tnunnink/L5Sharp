using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Member<TDataType> : IMember<TDataType>, IEquatable<Member<TDataType>> where TDataType : IDataType
    {
        private Member(string name, Dimensions dimensions = null, ExternalAccess externalAccess = null,
            string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            Dimensions = dimensions ?? Dimensions.Empty;
            ExternalAccess = externalAccess != null ? externalAccess : ExternalAccess.ReadWrite;
            Description = description;
        }

        public Member(string name, TDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            : this(name, dimensions, externalAccess, description)
        {
            DataType = dataType;

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            var elements = new List<IMember<TDataType>>(Dimensions);
            for (var i = 0; i < Dimensions; i++)
                elements.Add(new Member<TDataType>($"{name}[{i}]", dataType, Dimensions.Empty, radix, externalAccess,
                    description));
            Elements = elements.ToArray();
        }

        public Member(string name, IReadOnlyCollection<TDataType> dataTypes, Dimensions dimensions, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            : this(name, dimensions, externalAccess, description)
        {
            DataType = dataTypes.FirstOrDefault();
            
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            if (Dimensions.AreEmpty)
                throw new ArgumentException("Dimensions must have length greater than zero");
            if (Dimensions.AreMultiDimensional)
                throw new ArgumentException("Member Dimensions must be of a single dimension");
            if (Dimensions.Length != dataTypes.Count)
                throw new ArgumentException("Dimensions size must match provided data type array length");

            var elements = new List<IMember<TDataType>>(Dimensions);
            elements.AddRange(dataTypes.Select((t, i) =>
                new Member<TDataType>($"{name}[{i}]", t, Dimensions.Empty, radix, externalAccess, description)));
            Elements = elements.ToArray();
        }

        public string Name { get; }
        public string Description { get; }
        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; }
        public IMember<TDataType>[] Elements { get; }

        public bool Equals(Member<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimensions == other.Dimensions &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member<TDataType>)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimensions, Radix, ExternalAccess, Description);
        }

        public static bool operator ==(Member<TDataType> left, Member<TDataType> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member<TDataType> left, Member<TDataType> right)
        {
            return !Equals(left, right);
        }
    }

    public static class Member
    {
        public static IMember<IDataType> New(string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Member<TDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IMember<IDataType> OfType(string name, IDataType[] dataTypes, Dimensions dimension,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataTypes, dimension, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name, Dimensions dimension,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            return new Member<TDataType>(name, dimension.ArrayOf<TDataType>(), dimension, radix, externalAccess,
                description);
        }
    }
}