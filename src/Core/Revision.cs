using System;

namespace L5Sharp.Core
{
    public class Revision : IEquatable<Revision>
    {
        public Revision(byte major = 1, byte minor = 1)
        {
            Major = IsValidMajor(major)
                ? major
                : throw new ArgumentOutOfRangeException(nameof(major), $"'{major}' is not between 1 and 127");

            Minor = IsValidMinor(minor)
                ? minor
                : throw new ArgumentOutOfRangeException(nameof(minor), $"'{minor}' is not between 1 and 255");
        }

        public byte Major { get; }
        public byte Minor { get; }

        public Revision ChangeMajor(byte major)
        {
            if (!IsValidMajor(major))
                throw new ArgumentOutOfRangeException(nameof(major), $"'{major}' is not between 1 and 127");

            return new Revision(major, Minor);
        }

        public Revision ChangeMinor(byte minor)
        {
            if (!IsValidMinor(minor))
                throw new ArgumentOutOfRangeException(nameof(minor), $"'{minor}' is not between 1 and 255");

            return new Revision(Major, minor);
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}";
        }

        public bool Equals(Revision other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major == other.Major && Minor == other.Minor;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Revision)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Major, Minor);
        }

        public static bool operator ==(Revision left, Revision right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Revision left, Revision right)
        {
            return !Equals(left, right);
        }

        private static bool IsValidMajor(byte major)
        {
            return major >= 1 && major <= 127;
        }

        private static bool IsValidMinor(byte major)
        {
            return major >= 1 && major <= 255;
        }
    }
}