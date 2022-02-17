using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// Represents an alpha-numeric string identification number of a Logix <see cref="Module"/> component.
    /// </summary>
    /// <remarks>
    /// For most Rockwell devices, the catalog follows a standard pattern, which contains a <see cref="Bulletin"/> prefix
    /// followed by a set of characters indicating the device type or category, and optionally characters to indicate other
    /// components of features of the Module. However, some Module, and typically those not made by Rockwell, don't adhere
    /// to the same conventions. 
    /// </remarks>
    public class CatalogNumber : IEquatable<CatalogNumber>
    {
        private const string CatalogPattern = @"^([\d]{4})-([A-Z]+)(\d+)([A-Z]*)$";
        private const string BulletinPattern = @"[\d]{4}";
        private readonly string _catalogNumber;


        /// <summary>
        /// Creates a new <see cref="CatalogNumber"/> instance with the provided input value. 
        /// </summary>
        /// <param name="catalogNumber">The value of the catalog number to create.</param>
        /// <exception cref="ArgumentNullException">catalogNumber is null or empty.</exception>
        public CatalogNumber(string catalogNumber)
        {
            if (string.IsNullOrEmpty(catalogNumber))
                throw new ArgumentNullException(nameof(catalogNumber));

            _catalogNumber = catalogNumber;
        }

        /// <summary>
        /// Gets the value of the <see cref="Bulletin"/> that could be inferred from the current <see cref="CatalogNumber"/>.
        /// </summary>
        /// <remarks>
        /// Since not all Modules have a 4 digit <see cref="Bulletin"/>, this is a nullable property. If not identifiable,
        /// this value will remain null.
        /// </remarks>
        public Bulletin? Bulletin => Regex.Match(_catalogNumber, BulletinPattern, RegexOptions.Compiled).Value;


        /// <summary>
        /// Converts a <see cref="CatalogNumber"/> value to a <see cref="string"/> value.
        /// </summary>
        /// <param name="value">The value of the <see cref="CatalogNumber"/> to convert.</param>
        /// <returns>A new <see cref="string"/> value representing the value of the <see cref="CatalogNumber"/>.</returns>
        public static implicit operator string(CatalogNumber value) => value._catalogNumber;

        /// <summary>
        /// Converts a <see cref="string"/> value to a <see cref="CatalogNumber"/> value.
        /// </summary>
        /// <param name="value">The value of the <see cref="string"/> to convert.</param>
        /// <returns>A new <see cref="CatalogNumber"/> value representing the value of the <see cref="string"/>.</returns>
        public static implicit operator CatalogNumber(string value) => new(value);

        /// <inheritdoc />
        public override string ToString() => _catalogNumber;

        /// <inheritdoc />
        public bool Equals(CatalogNumber? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _catalogNumber == other._catalogNumber;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) => Equals(obj as CatalogNumber);

        /// <inheritdoc />
        public override int GetHashCode() => _catalogNumber.GetHashCode();

        /// <summary>
        /// Determines if the provided objects are equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are equal; otherwise, false.</returns>
        public static bool operator ==(CatalogNumber? left, CatalogNumber? right) => Equals(left, right);

        /// <summary>
        /// Determines if the provided objects are not equal.
        /// </summary>
        /// <param name="left">An object to compare.</param>
        /// <param name="right">An object to compare.</param>
        /// <returns>true if the provided objects are not equal; otherwise, false.</returns>
        public static bool operator !=(CatalogNumber? left, CatalogNumber? right) => !Equals(left, right);
    }
}