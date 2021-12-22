using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    /// <summary>
    /// An enumeration of all Logix ExternalAccess options.
    /// <remarks>
    /// ExternalAccess is a Logix setting that determines the ability to read from or write to a given component.
    /// </remarks>
    /// </summary>
    public sealed class ExternalAccess : SmartEnum<ExternalAccess>
    {
        private ExternalAccess(string name, int value) : base(name, value)
        {
        }

        /// <summary>
        /// Determines whether the current instance has more restrictive access the another. 
        /// </summary>
        /// <remarks>
        /// External access restriction is based on the enum integer value.
        /// The restriction levels are less restrictive in increasing order of value.
        /// In other words, <see cref="None"/> is more restrictive than <see cref="ReadOnly"/>
        /// which are both more restrictive than <see cref="ReadWrite"/>.
        /// </remarks>
        /// <param name="other">The <c>ExternalAccess</c> option to compare with.</param>
        /// <returns>
        /// true if the current access is more restrictive than the other; otherwise, false
        /// </returns>
        public bool IsMoreRestrictive(ExternalAccess? other)
        {
            return other is not null && Value < other.Value;
        }

        /// <summary>
        /// Represents the absence of a <c>ExternalAccess</c> value.
        /// </summary>
        public static readonly ExternalAccess Null = new ExternalAccess("Null", -1);

        /// <summary>
        /// Represents no read or write access.
        /// </summary>
        public static readonly ExternalAccess None = new ExternalAccess("None", 0);

        /// <summary>
        /// Represents read but not write access.
        /// </summary>
        public static readonly ExternalAccess ReadOnly = new ExternalAccess("Read Only", 1);

        /// <summary>
        /// Represents read and write access.
        /// </summary>
        public static readonly ExternalAccess ReadWrite = new ExternalAccess("Read/Write", 2);
    }
}