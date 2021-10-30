using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataTypeMember : LogixComponent, IDataTypeMember, IEquatable<DataTypeMember>
    {
        private IDataType _dataType;
        private Dimensions _dimensions;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public DataTypeMember(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null) : base(name, description)
        {
            _dataType = dataType ?? Predefined.Undefined;
            _dimensions = dimension ?? Dimensions.Empty;
            _radix = radix != null ? radix.IsValidForType(_dataType) ? 
                    radix : throw new RadixNotSupportedException(radix, _dataType)
                : _dataType is IPredefined predefined ? predefined.DefaultRadix : Radix.Null;
            _externalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
        }

        public IDataType DataType => _dataType;

        public Dimensions Dimensions => _dimensions;

        public Radix Radix => _radix;

        public ExternalAccess ExternalAccess => _externalAccess;

        public void SetDataType(IDataType dataType)
        {
            dataType ??= Predefined.Undefined;

            SetProperty(ref _dataType, dataType, nameof(DataType));
        }

        public void SetDimensions(Dimensions dimensions)
        {
            dimensions ??= Dimensions.Empty;

            if (dimensions.IsMultiDimensional)
                throw new InvalidOperationException("Can not set data type member to a multidimensional array");

            SetProperty(ref _dimensions, dimensions, nameof(Dimensions));
        }

        public void SetRadix(Radix radix)
        {
            radix ??= Radix.Null;

            Validate.Radix(radix, _dataType);

            SetProperty(ref _radix, radix, nameof(Radix));
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access property can not be null");

            SetProperty(ref _externalAccess, externalAccess, nameof(ExternalAccess));
        }

        public bool Equals(DataTypeMember other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(_dataType, other._dataType) && Equals(_radix, other._radix) &&
                   Equals(_externalAccess, other._externalAccess) && Description == other.Description &&
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