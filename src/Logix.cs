using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using String = L5Sharp.Types.String;

[assembly: InternalsVisibleTo("L5Sharp.Internal.Tests")]
    
namespace L5Sharp
{
    /// <summary>
    /// A global static class for accessing predefined Logix data types and instructions. Consumers may also register
    /// their own data types and instructions here to make available to the rest of the library.
    /// </summary>
    public static class Logix
    {
        /// <summary>
        /// Registers a data type to the global Logix context.
        /// </summary>
        /// <param name="name">The name of the data type to register. The name must be unique (i.e., not the name of a current data type.</param>
        /// <param name="factory">A factory delegate for creating the registered data type.</param>
        /// <exception cref="ArgumentException">Thrown when name is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when factory is null.</exception>
        /// <exception cref="ComponentNameCollisionException">Thrown when the provided name already exists in the registry</exception>
        public static void Register(string name, Func<IDataType> factory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            if (factory == null)
                throw new ArgumentNullException(nameof(factory), "Factory delegate can not be null or empty");

            if (DataType.Registry.ContainsKey(name))
                throw new ComponentNameCollisionException(name, typeof(IDataType));

            DataType.Registry.Add(name, factory);
        }

        /*/// <summary>
        /// Registers an instruction to the global Logix context
        /// </summary>
        /// <param name="name">The name of the instruction to register. The name must be unique (i.e., not the name of a current instruction</param>
        /// <param name="factory">A factory delegate for creating the registered instruction</param>
        /// <exception cref="ArgumentException">When name is null or empty</exception>
        /// <exception cref="ArgumentNullException">When factory is null</exception>
        /// <exception cref="ComponentNameCollisionException">When the provided name already exists in the registry</exception>
        public static void Register(string name, Func<IInstruction> factory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            if (factory == null)
                throw new ArgumentNullException(nameof(factory), "Factory delegate can not be null");

            if (Instruction.Registry.ContainsKey(name))
                throw new ComponentNameCollisionException(name, typeof(IInstruction));

            Instruction.Registry.Add(name, factory);
        }*/


        /// <summary>
        /// Predefined set of Logix Data Types. 
        /// </summary>
        public static class DataType
        {
            internal static readonly Dictionary<string, Func<IDataType>> Registry =
                new(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Bool), () => new Bool() },
                    { "Bit", () => new Bool() }, //Bit is a valid type that appears in the L5X and is the same as a Bool
                    { nameof(Sint), () => new Sint() },
                    { nameof(Int), () => new Int() },
                    { nameof(Dint), () => new Dint() },
                    { nameof(Lint), () => new Lint() },
                    { nameof(Real), () => new Real() },
                    { nameof(String), () => new String() },
                    { nameof(Counter), () => new Counter() },
                    { nameof(Timer), () => new Timer() },
                    { nameof(Alarm), () => new Alarm() },
                    { nameof(Message), () => new Message() },
                    { nameof(Control), () => new Control() }
                };

            /// <summary>
            /// List of all registered Logix Data Types.
            /// </summary>
            public static IEnumerable<string> List => Registry.Keys.AsEnumerable();

            /// <summary>
            /// Determines if the provided data type name is registered in the current Logix context.
            /// </summary>
            /// <param name="name">The name of the data type to find.</param>
            /// <returns>true if the <see cref="IDataType"/> is defined in the registered collection.</returns>
            public static bool Contains(string name) => Registry.ContainsKey(name);

            /// <summary>
            /// Creates an instance of the given data type.
            /// </summary>
            /// <param name="name">The name of the data type to create.</param>
            /// <returns>
            /// An instance of the <see cref="IDataType"/> if it is registered.
            /// Otherwise, null.
            /// </returns>
            public static IDataType Instantiate(string name)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name));

                return Registry.ContainsKey(name) ? Registry[name].Invoke() : new Undefined(name);
            }
        }

        
        /*public static class Instruction
        {
            internal static readonly Dictionary<string, Func<IInstruction>> Registry =
                new Dictionary<string, Func<IInstruction>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(XIC), () => new XIC() },
                    { nameof(XIO), () => new XIO() },
                    { nameof(OTE), () => new OTE() },
                    { nameof(OTL), () => new OTL() },
                    { nameof(OTU), () => new OTU() },
                    { nameof(ONS), () => new ONS() },
                    { nameof(OSR), () => new OSR() },
                    { nameof(OSF), () => new OSF() },
                    { nameof(MOV), () => new MOV() }
                };

            /// <summary>
            /// List of all registered Logix Instructions.
            /// </summary>
            public static IEnumerable<string> List => Registry.Keys.AsEnumerable();

            /// <summary>
            /// Determines if the provided instruction name is registered in the current Logix context.
            /// </summary>
            /// <param name="name">The name of the instruction to find.</param>
            /// <returns>true if the <see cref="IInstruction"/> is defined in the registered collection.</returns>
            public static bool Contains(string name) => Registry.ContainsKey(name);

            /// <summary>
            /// Creates an instance of the given instruction.
            /// </summary>
            /// <param name="name">The name of the instruction to create.</param>
            /// An instance of the <see cref="IInstruction"/> if it is registered.
            /// Otherwise, null.
            public static IInstruction Instantiate(string name)
            {
                return Registry.ContainsKey(name) ? Registry[name].Invoke() : null;
            }
        }*/
    }
}