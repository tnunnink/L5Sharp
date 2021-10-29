using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class DataTypeMember : Component, IDataTypeMember, IEquatable<DataTypeMember>
    {
        private IDataType _dataType;
        private Dimensions _dimension;
        private Radix _radix;
        private ExternalAccess _externalAccess;

        public DataTypeMember(string name, IDataType dataType, Dimensions dimension = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null) : base(name, description)
        {
            _dataType = dataType ?? Predefined.Undefined;
            _dimension = dimension ?? Dimensions.Empty;
            _radix = !(_dataType is IPredefined predefined) ? Radix.Null :
                radix == null ? predefined.DefaultRadix : radix;
            _externalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
        }

        public IDataType DataType => _dataType;

        public Dimensions Dimension => _dimension;

        public Radix Radix => _radix;

        public ExternalAccess ExternalAccess => _externalAccess;

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetDescription(string description)
        {
            throw new NotImplementedException();
        }

        public void SetDataType(IDataType dataType)
        {
            dataType ??= Predefined.Undefined;

            SetProperty(ref _dataType, dataType);
        }

        public void SetDimensions(Dimensions dimensions)
        {
            dimensions ??= Dimensions.Empty;

            if (dimensions.IsMultiDimensional)
                throw new InvalidOperationException("Can not set data type member to a multidimensional array");

            SetProperty(ref _dimension, dimensions);
        }

        public void SetRadix(Radix radix)
        {
            radix ??= Radix.Null;

            Validate.Radix(radix, _dataType);

            SetProperty(ref _radix, radix);
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access property can not be null");

            SetProperty(ref _externalAccess, externalAccess);
        }

        public bool Equals(DataTypeMember other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(_dataType, other._dataType) && Equals(_radix, other._radix) &&
                   Equals(_externalAccess, other._externalAccess) && Description == other.Description &&
                   Dimension == other.Dimension;
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
            hashCode.Add(Dimension);
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