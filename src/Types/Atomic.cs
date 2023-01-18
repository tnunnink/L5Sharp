using System;
using System.Collections.Generic;
using System.ComponentModel;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types
{
    /// <summary>
    /// A static class containing methods related to the logix value type or <see cref="AtomicType"/>.
    /// </summary>
    public static class Atomic
    {
        private static readonly Dictionary<string, Type> AtomicTypes = new()
        {
            { nameof(BOOL), typeof(BOOL) },
            { nameof(SINT), typeof(SINT) },
            { nameof(INT), typeof(INT) },
            { nameof(DINT), typeof(DINT) },
            { nameof(LINT), typeof(LINT) },
            { nameof(USINT), typeof(USINT) },
            { nameof(UINT), typeof(UINT) },
            { nameof(UDINT), typeof(UDINT) },
            { nameof(ULINT), typeof(ULINT) },
            { nameof(REAL), typeof(REAL) }
        };

        /// <summary>
        /// Parses a string input to a <see cref="AtomicType"/> value based on the format of the input value.
        /// </summary>
        /// <param name="input">The string value to parse.</param>
        /// <param name="radix">The optional radix format of the atomic type.
        /// If not specified, the radix will be inferred based on the format of <c>input</c>.</param>
        /// <returns> A <see cref="AtomicType"/> representing the value of the parsed input.</returns>
        /// <remarks>
        /// This method determines the radix based on patterns in the input string. For example, if the string input
        /// starts with the specifier '2#', this method will forward the call to <see cref="Parse"/> for the Binary Radix
        /// and return the result. If no radix can be determined from the input string, the call is forwarded to the
        /// </remarks>
        public static AtomicType Parse(string input, Radix? radix = null)
        {
            radix ??= Radix.Infer(input);

            var atomic = radix.Parse(input);
            atomic.Radix = radix;
            
            return atomic;
        }

        public static AtomicType Parse(string name, string input, Radix? radix = null)
        {
            radix ??= Radix.Infer(input);

            if (!AtomicTypes.TryGetValue(name, out var type))
                throw new ArgumentException($"The specified atomic type name {name} is not a valid atomic type.");

            var converter = TypeDescriptor.GetConverter(type);

            if (converter.ConvertFrom(radix.Parse(input)) is not AtomicType atomic)
                throw new InvalidOperationException($"Could not convert input {input} to atomic type {type}");

            atomic.Radix = radix;
            return atomic;
        }

        public static AtomicType Parse<TAtomic>(string input, Radix? radix = null) where TAtomic : AtomicType
        {
            radix ??= Radix.Infer(input);

            var converter = TypeDescriptor.GetConverter(typeof(TAtomic));

            if (converter.ConvertFrom(radix.Parse(input)) is not AtomicType atomic)
                throw new InvalidOperationException(
                    $"Could not convert input {input} to atomic type {typeof(TAtomic)}");

            atomic.Radix = radix;
            return atomic;
        }

        /// <summary>
        /// Determines whether the specified type name is an <see cref="AtomicType"/> logix type.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns><c>true</c> if <c>typeName</c> represents the name of an atomic type; otherwise, <c>false</c>.</returns>
        public static bool IsAtomic(string typeName) => AtomicTypes.ContainsKey(typeName);
    }
}