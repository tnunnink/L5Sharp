using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// External Access enumeration options.
    /// <remarks>
    /// External Access is a Logix setting that determines the ability to read from or write to a component (i.e. Tags)
    /// </remarks>
    /// </summary>
    public sealed class ExternalAccess : SmartEnum<ExternalAccess>
    {
        private ExternalAccess(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Determines whether the current instance has more restrictive access the another 
        /// </summary>
        /// <remarks>
        /// External access restriction is based on the enum integer value.
        /// The restriction levels are less restrictive in increasing order of value.
        /// <see cref="None"/> is more restrictive than <see cref="ReadOnly"/> which are both more restrictive than <see cref="ReadWrite"/>.
        /// </remarks>
        /// <param name="other">The other instance of <see cref="ExternalAccess"/> to compare</param>
        /// <returns>
        /// <see langword="True"/> if the current access is more restrictive than the other. Otherwise, False
        /// </returns>
        public bool IsMoreRestrictive(ExternalAccess other)
        {
            return other != null && Value < other.Value;
        }

        /// <summary>
        /// <see cref="None"/> represents the absence of a value
        /// </summary>
        public static readonly ExternalAccess Null = new ExternalAccess("Null", -1);

        /// <summary>
        /// <see cref="None"/> represents no read or write access 
        /// </summary>
        public static readonly ExternalAccess None = new ExternalAccess("None", 0);

        /// <summary>
        /// <see cref="ReadOnly"/> represents read but not write access 
        /// </summary>
        public static readonly ExternalAccess ReadOnly = new ExternalAccess("Read Only", 1);

        /// <summary>
        /// <see cref="ReadOnly"/> represents read and write access
        /// </summary>
        public static readonly ExternalAccess ReadWrite = new ExternalAccess("Read/Write", 2);
    }
}