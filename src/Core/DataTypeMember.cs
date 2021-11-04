using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataTypeMember : LogixComponent, IDataTypeMember, IEquatable<DataTypeMember>
    {
        public DataTypeMember(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null) : base(name, description)
        {
            DataType = dataType ?? Logix.DataType.Undefined;
            Dimensions = dimension ?? Dimensions.Empty;
            
            Radix = radix != null 
                ? radix.IsValidForType(DataType) 
                    ? radix : throw new RadixNotSupportedException(radix, DataType)
                : DataType is IAtomic atomic 
                    ? atomic.DefaultRadix : Radix.Null;
            
            ExternalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
        }

        public IDataType DataType { get; private set; }

        public Dimensions Dimensions { get; private set; }

        public Radix Radix { get; private set; }

        public ExternalAccess ExternalAccess { get; private set; }

        public void SetDataType(IDataType dataType)
        {
            dataType ??= Logix.DataType.Undefined;

            DataType = dataType;
        }

        public void SetDimensions(Dimensions dimensions)
        {
            dimensions ??= Dimensions.Empty;

            if (dimensions.IsMultiDimensional)
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

        public bool Equals(DataTypeMember other)
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
            return obj.GetType() == GetType() && Equals((DataTypeMember)obj);
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

        public static bool operator ==(DataTypeMember left, DataTypeMember right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataTypeMember left, DataTypeMember right)
        {
            return !Equals(left, right);
        }
    }
}