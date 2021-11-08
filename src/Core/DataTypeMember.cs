using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataTypeMember<TDataType> : LogixComponent,
        IDataTypeMember<TDataType>, IEquatable<DataTypeMember<TDataType>>
        where TDataType : IDataType
    {
        
        public DataTypeMember(string name, TDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null) : base(name, description)
        {
            DataType = dataType;
            Dimensions = dimension ?? Dimensions.Empty;

            Radix = radix != null
                ? radix.IsValidForType(DataType)
                    ? radix
                    : throw new RadixNotSupportedException(radix, DataType)
                : DataType is IAtomic atomic
                    ? Radix.Default(atomic)
                    : Radix.Null;

            ExternalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
        }


        public TDataType DataType { get; }
        public Dimensions Dimensions { get; private set; }
        public Radix Radix { get; private set; }
        public ExternalAccess ExternalAccess { get; private set; }
        public IMember<TDataType>[] Elements { get; }

        public void SetDimensions(Dimensions dimensions)
        {
            dimensions ??= Dimensions.Empty;

            if (dimensions.AreMultiDimensional)
                throw new InvalidOperationException("Can not set data type member to a multidimensional array");

            Dimensions = dimensions;
        }

        public void SetRadix(Radix radix)
        {
            radix ??= Radix.Null;

            Validate.Radix(radix, DataType);

            Radix = radix;
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access property can not be null");

            ExternalAccess = externalAccess;
        }

        public bool Equals(DataTypeMember<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Equals(Radix, other.Radix) &&
                   Equals(ExternalAccess, other.ExternalAccess) && Description == other.Description &&
                   Dimensions == other.Dimensions;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataTypeMember<TDataType>)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(DataType);
            hashCode.Add(Radix);
            hashCode.Add(ExternalAccess);
            hashCode.Add(Dimensions);
            return hashCode.ToHashCode();
        }

        public static bool operator ==(DataTypeMember<TDataType> left, DataTypeMember<TDataType> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataTypeMember<TDataType> left, DataTypeMember<TDataType> right)
        {
            return !Equals(left, right);
        }
    }

    public static class DataTypeMember
    {
        public static IDataTypeMember<IDataType> New(string name, IDataType dataType = null,
            Dimensions dimension = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new DataTypeMember<IDataType>(name, dataType, dimension, radix, externalAccess, description);
        }

        public static IDataTypeMember<TDataType> New<TDataType>(string name, Dimensions dimension = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new DataTypeMember<TDataType>(name, dataType, dimension, radix, externalAccess, description);
        }
    }
}