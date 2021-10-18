using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Member : ComponentBase, IMember, IEquatable<Member>
    {
        private IDataType _dataType;
        private Radix _radix;
        private ushort _dimension;
        private ExternalAccess _externalAccess;

        public Member(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null) : base(name, description)
        {
            DataType = dataType ?? Predefined.Undefined;
            Dimension = dimension;
            Radix = DataType.Equals(Predefined.Undefined) ? Radix.Null : radix == null ? DataType.DefaultRadix : radix;
            ExternalAccess = externalAccess == null ? ExternalAccess.ReadWrite : externalAccess;
        }

        public IDataType DataType
        {
            get => _dataType;
            set
            {
                value ??= Predefined.Undefined;
                SetProperty(ref _dataType, value);
            }
        }

        public ushort Dimension
        {
            get => _dimension;
            set => SetProperty(ref _dimension, value);
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                Validate.Radix(value, _dataType);
                SetProperty(ref _radix, value);
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "ExternalAccess property can not be null");
                SetProperty(ref _externalAccess, value);
            }
        }

        public bool Equals(Member other)
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
            return obj.GetType() == GetType() && Equals((Member)obj);
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

        public static bool operator ==(Member left, Member right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !Equals(left, right);
        }
    }
}