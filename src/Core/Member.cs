using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Member : IMember, IEquatable<Member>
    {
        private string _name;
        private IDataType _dataType;
        private Radix _radix;
        private ushort _dimension;
        private ExternalAccess _externalAccess;
        private string _description;

        public Member(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess access = null, string description = null)
        {
            Name = name;
            DataType = dataType ?? Predefined.Undefined;
            Dimension = dimension;
            Radix = DataType.Equals(Predefined.Undefined) ? Radix.Null : radix == null ? DataType.DefaultRadix : radix;
            ExternalAccess = access == null ? ExternalAccess.ReadWrite : access;
            Description = description;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                
                Validate.Name(value);
                
                _name = value;
                
                RaiseUpdated();
            }
        }

        public IDataType DataType
        {
            get => _dataType;
            set
            {
                if (_dataType != null && _dataType.Equals(value)) return;
                
                _dataType = value ?? Predefined.Undefined;
                
                RaiseUpdated();
            }
        }

        public ushort Dimension
        {
            get => _dimension;
            set
            {
                if (_dimension == value) return;
                
                _dimension = value;
                
                RaiseUpdated();
            }
        }

        public Radix Radix
        {
            get => _radix;
            set
            {
                if (_radix == value) return;
                
                Validate.Radix(value, _dataType);
                
                _radix = value;
                
                RaiseUpdated();
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                if (_externalAccess == value) return;
                
                _externalAccess = value ?? throw new ArgumentNullException(nameof(value),
                    "ExternalAccess property can not be null");
                RaiseUpdated();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;
                
                _description = value;
                RaiseUpdated();
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

        public event EventHandler Updated;

        private void RaiseUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}