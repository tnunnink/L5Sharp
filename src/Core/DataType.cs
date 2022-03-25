using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    /// <summary>
    /// A static class containing known <see cref="IDataType"/> objects that can be instantiated upon request.
    /// </summary>
    public static class DataType
    {
        private static readonly Dictionary<string, IDataType> Registry = new(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(BOOL), new BOOL() },
            { "Bit", new BOOL() },
            { nameof(SINT), new SINT() },
            { nameof(USINT), new USINT() },
            { nameof(INT), new INT() },
            { nameof(UINT), new UINT() },
            { nameof(DINT), new DINT() },
            { nameof(UDINT), new UDINT() },
            { nameof(LINT), new LINT() },
            { nameof(ULINT), new ULINT() },
            { nameof(REAL), new REAL() },
            { nameof(STRING), new STRING() },
            { nameof(COUNTER), new COUNTER() },
            { nameof(TIMER), new TIMER() },
            { nameof(ALARM), new ALARM() },
            { nameof(MESSAGE), new MESSAGE() },
            { nameof(CONTROL), new CONTROL() }
        };

        /// <summary>
        /// List of all registered <see cref="IDataType"/> names.
        /// </summary>
        public static IEnumerable<string> All => Registry.Keys.AsEnumerable();

        /// <summary>
        /// Gets a list of all registered data type names that are <see cref="DataTypeClass.Atomic"/> types. 
        /// </summary>
        public static IEnumerable<string> Atomics =>
            Registry.Where(t => t.Value.Class.Equals(DataTypeClass.Atomic)).Select(t => t.Key);

        /// <summary>
        /// Gets a list of all registered data type names that are <see cref="DataTypeClass.Predefined"/> types. 
        /// </summary>
        public static IEnumerable<string> Predefined =>
            Registry.Where(t => t.Value.Class.Equals(DataTypeClass.Predefined)).Select(t => t.Key);

        /// <summary>
        /// Determines if the provided data type name is registered in the current Logix context.
        /// </summary>
        /// <param name="name">The name of the data type to find.</param>
        /// <returns>true if the <see cref="IDataType"/> is defined in the registered collection.</returns>
        public static bool IsDefined(string name) => Registry.ContainsKey(name);

        /// <summary>
        /// Creates a new <see cref="IDataType"/> instance using the provided name.
        /// </summary>
        /// <param name="name">The name of the data type to create.</param>
        /// <returns>
        /// If the provided name is a type that is part of the predefined registry,
        /// a new new <see cref="IDataType"/> instance representing the specified name;
        /// otherwise a <see cref="UNDEFINED"/> instance wrapping the provided name.
        /// </returns>
        /// <exception cref="ArgumentException">name is null or empty.</exception>
        public static IDataType Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            return Registry.ContainsKey(name) ? Registry[name].Instantiate() : new UNDEFINED(name);
        }

        /// <summary>
        /// Creates a new <see cref="IAtomicType"/> with the provided type name, radix, and value.
        /// </summary>
        /// <param name="name">The name of the atomic type to create.</param>
        /// <param name="value">The value to initialize the type with.</param>
        /// <returns>A new <see cref="IAtomicType"/> value instance with the provided arguments.</returns>
        /// <exception cref="ArgumentException">name is null or empty -or- name is not a valid atomic type name.</exception>
        public static IAtomicType Atomic(string name, object? value = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            if (!Atomics.Any(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"The data type name '{name}' is not an atomic type");
            
            var type = (IAtomicType)Registry[name].Instantiate();

            if (value is not null)
                type.SetValue(value);

            return type;
        }

        /// <summary>
        /// Creates a new <see cref="IComplexType"/> with the provided data type name.
        /// </summary>
        /// <param name="name">The name of the predefined complex type to create.</param>
        /// <returns>A new <see cref="IComplexType"/> instance identified by the provided name.</returns>
        /// <exception cref="ArgumentException">name is null or empty -or- name is not a valid predefined complex type name.</exception>
        public static IComplexType Complex(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            if (!Predefined.Any(n => string.Equals(n, name, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"The data type name '{name}' is not a predefined type");

            return (IComplexType)Registry[name].Instantiate();
        }
    }
}