using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Types
{
    /// <summary>
    /// A static class containing known <see cref="IDataType"/> objects that can be instantiated upon request.
    /// </summary>
    public static class LogixType
    {
        private static readonly Dictionary<string, ILogixType> Registry = new(StringComparer.OrdinalIgnoreCase)
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
        /// List of all predefined <see cref="ILogixType"/> names.
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
        /// <returns>true if the <see cref="ILogixType"/> is defined in the registered collection.</returns>
        public static bool IsDefined(string name) => Registry.ContainsKey(name);
    }
}