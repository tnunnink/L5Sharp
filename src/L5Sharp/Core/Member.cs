using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Member : IMember, IEquatable<Member>, INotifyPropertyChanged
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
            DataType = dataType ?? Predefined.Null;
            Dimension = dimension;
            Radix = radix == null ? DataType.DefaultRadix : radix;
            ExternalAccess = access == null ? ExternalAccess.ReadWrite : access;
            Description = description ?? string.Empty;
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
            set
            {
                _dataType = value ?? Predefined.Null;
                OnPropertyChanged();
            }
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
                "ExternalAccess property can not be null");
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? throw new ArgumentNullException(nameof(value),
                "Description property can not be null");
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}