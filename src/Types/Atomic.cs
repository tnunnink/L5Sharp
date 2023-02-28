using System;
using System.Collections.Generic;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;

namespace L5Sharp.Types
{
    /// <summary>
    /// A static class containing methods related to the logix value type or <see cref="AtomicType"/>.
    /// </summary>
    public static class Atomic
    {
        private static readonly Dictionary<string, Func<string, Radix?, AtomicType>> Factories = new()
        {
            { nameof(BOOL), (s, r) => new BOOL(s, r) },
            { "BIT", (s, r) => new BOOL(s, r) },
            { nameof(SINT), (s, r) => new SINT(s, r) },
            { nameof(INT), (s, r) => new INT(s, r) },
            { nameof(DINT), (s, r) => new DINT(s, r) },
            { nameof(LINT), (s, r) => new LINT(s, r) },
            { nameof(USINT), (s, r) => new USINT(s, r) },
            { nameof(UINT), (s, r) => new UINT(s, r) },
            { nameof(UDINT), (s, r) => new UDINT(s, r) },
            { nameof(ULINT), (s, r) => new ULINT(s, r) },
            { nameof(REAL), (s, r) => new REAL(s, r) }
        };
        
        /// <summary>
        /// Parses a string input to a <see cref="AtomicType"/> value specified by the provided type name. 
        /// </summary>
        /// <param name="input">The string input value to parse.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, this method will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <returns>A converted <see cref="AtomicType"/> representing the value of the input string.</returns>
        /// <exception cref="ArgumentException"><c>name</c> is not a known/existing atomic type name.</exception>
        /// <exception cref="InvalidOperationException">The parsed atomic type value could not be converted to an atomic type.</exception>
        public static AtomicType Parse(string input, Radix? radix = null)
        {
            radix ??= Radix.Infer(input);
            return radix.Parse(input);
        }

        /// <summary>
        /// Parses a string input to a <see cref="AtomicType"/> value specified by the provided type name. 
        /// </summary>
        /// <param name="name">The name of the atomic type to parse.</param>
        /// <param name="input">The string input value to parse.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, this method will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <returns>A converted <see cref="AtomicType"/> representing the value of the input string.</returns>
        /// <exception cref="ArgumentException"><c>name</c> is not a known/existing atomic type name.</exception>
        /// <exception cref="InvalidOperationException">The parsed atomic type value could not be converted to an atomic type.</exception>
        public static AtomicType Parse(string name, string input, Radix? radix = null)
        {
            if (!Factories.TryGetValue(name, out var factory))
                throw new ArgumentException();

            return factory.Invoke(input, radix);
        }

        /// <summary>
        /// Parses a string input to the <see cref="AtomicType"/> value specified byt the generic type parameter. 
        /// </summary>
        /// <param name="input">The string input value to parse.</param>
        /// <param name="radix">The optional radix format of the input string. If not provided, this method will
        /// call <see cref="Enums.Radix.Infer"/> to attempt to determine the format.</param>
        /// <typeparam name="TAtomic">The <see cref="AtomicType"/> to convert the parsed value to.</typeparam>
        /// <returns>A <see cref="AtomicType"/> value converted to the specified generic type parameter.</returns>
        /// <exception cref="InvalidOperationException">The parsed atomic type value could not be converted to an atomic type.</exception>
        public static TAtomic Parse<TAtomic>(string input, Radix? radix = null) where TAtomic : AtomicType
        {
            if (!Factories.TryGetValue(typeof(TAtomic).Name, out var factory))
                throw new ArgumentException();

            return (TAtomic)factory.Invoke(input, radix);
        }

        /// <summary>
        /// Determines whether the specified type name is an <see cref="AtomicType"/> logix type.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns><c>true</c> if <c>typeName</c> represents the name of an atomic type; otherwise, <c>false</c>.</returns>
        public static bool IsAtomic(string typeName) => Factories.ContainsKey(typeName);
    }
}