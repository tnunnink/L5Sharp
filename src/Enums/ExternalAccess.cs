using System;
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
        /// Gets the most restrictive of the two provided <c>ExternalAccess</c> objects.
        /// </summary>
        /// <param name="first">The first <c>ExternalAccess</c> object to compare.</param>
        /// <param name="second">The second <c>ExternalAccess</c> object to compare.</param>
        /// <returns>first if <see cref="ExternalAccess.Value"/> is less than second; otherwise; second.</returns>
        /// <exception cref="ArgumentNullException">When either first or second is null.</exception>
        public static ExternalAccess MostRestrictive(ExternalAccess first, ExternalAccess second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));
            
            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return first.Value < second.Value ? first : second;
        }
        

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