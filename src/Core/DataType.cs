using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Enums;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using String = L5Sharp.Types.String;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataType
    {
        private static readonly Dictionary<string, IDataType> Registry = new(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(Bool), new Bool() },
            //Bit is a valid type that appears in the L5X and is the same as a Bool
            { "Bit", new Bool() },
            { nameof(Sint), new Sint() },
            { nameof(Int), new Int() },
            { nameof(Dint), new Dint() },
            { nameof(Lint), new Lint() },
            { nameof(Real), new Real() },
            { nameof(String), new String() },
            { nameof(Counter), new Counter() },
            { nameof(Timer), new Timer() },
            { nameof(Alarm), new Alarm() },
            { nameof(Message), new Message() },
            { nameof(Control), new Control() }
        };

        /// <summary>
        /// List of all registered <see cref="IDataType"/> names.
        /// </summary>
        public static IEnumerable<string> List => Registry.Keys.AsEnumerable();

        /// <summary>
        /// Gets a list of all <see cref="IAtomicType"/> names.
        /// </summary>
        public static IEnumerable<string> Atomics =>
            Registry.Where(t => t.Value.Class.Equals(DataTypeClass.Atomic)).Select(t => t.Key);

        /// <summary>
        /// Determines if the provided data type name is registered in the current Logix context.
        /// </summary>
        /// <param name="name">The name of the data type to find.</param>
        /// <returns>true if the <see cref="IDataType"/> is defined in the registered collection.</returns>
        public static bool Exists(string name) => Registry.ContainsKey(name);

        /// <summary>
        /// Determines if the provided data type name is the name of an <see cref="IAtomicType"/>.
        /// </summary>
        /// <param name="name">The name of the type to check.</param>
        /// <returns>true if the provided name is the name of a predefined atomic type; otherwise false.</returns>
        public static bool IsAtomic(string name) => Atomics.Contains(name);

        /// <summary>
        /// Creates a new <see cref="IDataType"/> using provided name.
        /// </summary>
        /// <param name="name">The name of the data type to create.</param>
        /// <returns>
        /// if the provided name is a valid registered data type, a new new instance of the specified type;
        /// otherwise a <see cref="Types.Undefined"/> type of the provided name.
        /// </returns>
        /// <exception cref="ArgumentNullException">name is null or empty.</exception>
        public static IDataType New(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            return Registry.ContainsKey(name) ? Registry[name].Instantiate() : new Undefined(name);
        }

        /// <summary>
        /// Registers a <see cref="IDataType"/> to the static DataType class.
        /// </summary>
        /// <param name="dataType">The <see cref="IDataType"/> to register.</param>
        /// <exception cref="ArgumentNullException">dataType is null.</exception>
        /// <exception cref="ComponentNameCollisionException">dataType name already exists.</exception>
        public static void Register(IDataType dataType)
        {
            if (dataType is null)
                throw new ArgumentNullException(nameof(dataType));

            if (Registry.ContainsKey(dataType.Name))
                throw new ComponentNameCollisionException(dataType.Name, typeof(IDataType));

            Registry.Add(dataType.Name, dataType);
        }
    }
}