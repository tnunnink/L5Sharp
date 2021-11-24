using System;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents a revision or a number that is expressed by a Major and Minor revision.
    /// </summary>
    public class Revision : IEquatable<Revision>
    {
        /// <summary>
        /// Creates a new instance of a <c>Revision</c> withe the optional major and minor versions.
        /// </summary>
        /// <param name="major">The value of the major revision.</param>
        /// <param name="minor">The value of the minor revision.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Revision(ushort major = 1, ushort minor = 0)
        {
            Major = major;
            Minor = minor;
        }

        /// <summary>
        /// Gets the value of the Major revision number.
        /// </summary>
        public ushort Major { get; }
        
        /// <summary>
        /// Gets the value of the Minor revision number.
        /// </summary>
        public ushort Minor { get; }

        /// <summary>
        /// Gets an instance of a default <c>Revision</c> (i.e. 1.0)
        /// </summary>
        public Revision Default => new Revision();
        

        /// <summary>
        /// Parses the string input into a new instance of <see cref="Revision"/>
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <returns></returns>
        public static Revision Parse(string value)
        {
            //todo validate input.
            
            var revisions = value.Split('.');
            
            var major = byte.Parse(revisions[0]);
            var minor = byte.Parse(revisions[1]);
            
            return new Revision(major, minor);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Major}.{Minor}";
        }

        /// <inheritdoc />
        public bool Equals(Revision other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Major == other.Major && Minor == other.Minor;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Revision)obj);
        }

        /// <inheritdoc />
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
        
        
    }
}