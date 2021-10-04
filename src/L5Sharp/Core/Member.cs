using System;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Member : IMember, IEquatable<Member>
    {
        private string _name;
        private IDataType _dataType;
        private Radix _radix;
        private ExternalAccess _externalAccess;
        private string _description;

        internal Member(string name, IDataType dataType, ushort dimension, Radix radix,
            ExternalAccess access, string description, bool hidden, string target, ushort bitNumber)
        {
            Name = name;
            DataType = dataType;
            Dimension = dimension;
            Radix = radix ?? dataType.DefaultRadix;
            ExternalAccess = access ?? ExternalAccess.ReadWrite;
            Description = description;
            Hidden = hidden;
            Target = target ?? string.Empty;
            BitNumber = bitNumber;
        }

        public Member(string name, IDataType dataType, ushort dimension = 0, Radix radix = null,
            ExternalAccess access = null, string description = null)
            : this(name, dataType, dimension, radix, access, description, false, string.Empty, 0)
        {
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public IDataType DataType
        {
            get => _dataType;
            set => _dataType = value ?? Predefined.Null;
        }

        public ushort Dimension { get; set; }

        public Radix Radix
        {
            get => _radix;
            set
            {
                Validate.Radix(value, _dataType);
                _radix = value;
            }
        }

        public ExternalAccess ExternalAccess
        {
            get => _externalAccess;
            set
            {
                Validate.ExternalAccess(value);
                _externalAccess = value;
            }
        }

        public string Description
        {
            get => _description;
            set { _description = value ?? string.Empty; }
        }

        internal bool Hidden { get; set; }
        internal string Target { get; set; }
        internal ushort BitNumber { get; set; }

        public bool Equals(Member other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name && Equals(_dataType, other._dataType) && Equals(_radix, other._radix) &&
                   Equals(_externalAccess, other._externalAccess) && _description == other._description &&
                   Dimension == other.Dimension && Hidden == other.Hidden && Target == other.Target &&
                   BitNumber == other.BitNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Member)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(DataType);
            hashCode.Add(Radix);
            hashCode.Add(ExternalAccess);
            hashCode.Add(Description);
            hashCode.Add(Dimension);
            hashCode.Add(Hidden);
            hashCode.Add(Target);
            hashCode.Add(BitNumber);
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