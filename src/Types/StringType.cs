using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Data;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types
{
    /// <summary>
    /// A logix type that represents a string structure type.
    /// </summary>
    [LogixSerializer(typeof(StringDataSerializer))]
    public class StringType : StructureType, IEnumerable<char>
    {
        /// <summary>
        /// Creates a new <see cref="StringType"/> instance with the provided data.
        /// </summary>
        /// <param name="name">The name of the string type.</param>
        /// <param name="value">The string value of the type.</param>
        public StringType(string name, string value) : base(name)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            LEN = new DINT(value.Length);
            DATA = !value.IsEmpty()
                ? new ArrayType<SINT>(value.ToSintArray())
                : new ArrayType<SINT>(new[] { new SINT(0) });
        }

        /// <inheritdoc />
        public sealed override DataTypeFamily Family => DataTypeFamily.String;

        /// <summary>
        /// Gets the character length value of the string. 
        /// </summary>
        /// <returns>A <see cref="DINT"/> logix atomic value representing the integer length of the string.</returns>
        public DINT LEN { get; }

        /// <summary>
        /// Gets the array of bytes that represent the ASCII encoded string value.
        /// </summary>
        /// <returns>An array of <see cref="SINT"/> logix atomic values representing the bytes of the string.</returns>
        public ArrayType<SINT> DATA { get; }

        /// <inheritdoc />
        public IEnumerator<char> GetEnumerator() => DATA.Select(s => (char)(sbyte)s).GetEnumerator();

        /// <inheritdoc />
        public override string ToString() => DATA.AsString();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}