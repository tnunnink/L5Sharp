using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;

namespace L5Sharp.Core
{
    public abstract class Atomic : IAtomic, IEquatable<Atomic>
    {
        private readonly Dictionary<string, Member> _members =
            new Dictionary<string, Member>(StringComparer.OrdinalIgnoreCase);
        
        private protected Atomic(string name, IEnumerable<Member> members = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");

            members ??= Array.Empty<Member>();

            foreach (var member in members)
            {
                if (_members.ContainsKey(member.Name))
                    throw new ComponentNameCollisionException(member.Name, member.GetType());

                _members.Add(member.Name, member);
            }
            
            if (!Logix.DataType.Contains(name))
                Logix.DataType.Register(this);
        }

        public string Name { get; }
        public string Description => string.Empty;
        public DataTypeFamily Family => DataTypeFamily.None;
        public DataTypeClass Class => DataTypeClass.Atomic;
        public virtual object DefaultValue => 0;
        public virtual Radix DefaultRadix => Radix.Decimal;
        public virtual TagDataFormat DataFormat => TagDataFormat.Decorated;
        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public virtual bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Binary
                   || radix == Radix.Octal
                   || radix == Radix.Decimal
                   || radix == Radix.Hex
                   || radix == Radix.Ascii;
        }

        public abstract bool IsValidValue(object value);
        
        public abstract object ParseValue(string value);

        public bool Equals(Atomic other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_members, other._members) && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Atomic)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_members, Name);
        }

        public static bool operator ==(Atomic left, Atomic right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Atomic left, Atomic right)
        {
            return !Equals(left, right);
        }
    }
}