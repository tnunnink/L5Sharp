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
            _dataType = dataType ?? Predefined.Null;
            Dimension = dimension;
            _radix = radix ?? _dataType.DefaultRadix;
            _externalAccess = access ?? ExternalAccess.ReadWrite;
            _description = description ?? string.Empty;
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
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Cannot set member name property to null");

                Validate.Name(value);

                var old = _name;
                _name = value;
                RaiseNameChanged(old, _name);
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
            set => _externalAccess = value ?? throw new ArgumentNullException(nameof(value),
                "Cannot set member ExternalAccess property to null");
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(value),
                "Cannot set member Description property to null");
        }

        internal event EventHandler<NameChangedEventArgs> NameChanged;
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
            return obj.GetType() == GetType() && Equals((Member)obj);
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

        public void RaiseNameChanged(string oldName, string newName)
        {
            NameChanged?.Invoke(this, new NameChangedEventArgs(oldName, newName));
        }
    }
}