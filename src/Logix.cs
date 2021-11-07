using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Instructions;
using L5Sharp.Types;
using String = L5Sharp.Types.String;

namespace L5Sharp
{
    public static class Logix
    {
        public static class DataType
        {
            private static readonly Dictionary<string, Func<IDataType>> Registry =
                new Dictionary<string, Func<IDataType>>(StringComparer.OrdinalIgnoreCase)
                {
                    { nameof(Bool), () => new Bool() },
                    { "Bit", () => new Bool() }, //Bit is a valid type that appears int the L5X. Its same as a Bool
                    { nameof(Sint), () => new Sint() },
                    { nameof(Int), () => new Int() },
                    { nameof(Dint), () => new Dint() },
                    { nameof(Lint), () => new Lint() },
                    { nameof(Real), () => new Real() },
                    { nameof(Undefined), () => new Undefined() },
                    { nameof(String), () => new String() },
                    { nameof(Counter), () => new Counter() },
                    { nameof(Timer), () => new Timer() },
                    { nameof(Alarm), () => new Alarm() }
                };

            /// <summary>
            /// Gets all registered data type names
            /// </summary>
            public static IEnumerable<string> Names => Registry.Keys.ToList();

            /// <summary>
            /// Registers a data type to the Logix context
            /// </summary>
            /// <param name="name">The name of the data type to register</param>
            /// <param name="create">A delegate for creating the registered data type</param>
            /// <exception cref="ArgumentException">When name is null or empty</exception>
            /// <exception cref="ArgumentNullException">When create is null</exception>
            /// <exception cref="ComponentNameCollisionException">When the provided name already exists in the registry</exception>
            public static void Register(string name, Func<IDataType> create)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Name can not be null or empty");

                if (create == null)
                    throw new ArgumentNullException(nameof(create), "Create delegate can not be null or empty");

                if (Registry.ContainsKey(name))
                    throw new ComponentNameCollisionException(name, typeof(IDataType));

                Registry.Add(name, create);
            }

            /// <summary>
            /// Determines if the data type name is registered in the current Logix context
            /// </summary>
            /// <param name="name">The name of the data type to search</param>
            /// <returns>True when the registry contains the name of the data type provided</returns>
            public static bool Contains(string name)
            {
                return Registry.ContainsKey(name);
            }

            /// <summary>
            /// Creates an instance of the given data type
            /// </summary>
            /// <param name="name">The name of the data type to create</param>
            /// <returns>An instance of the IDatatype if it is registered. An instance of Undefined otherwise</returns>
            public static IDataType Create(string name)
            {
                return Registry.ContainsKey(name) ? Registry[name].Invoke() : new Undefined();
            }

            private static Type FindAssemblyType(string name)
            {
                return GetAssemblyTypes().SingleOrDefault(t => t.Name == name);
            }

            private static IEnumerable<Type> GetAssemblyTypes()
            {
                return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.IsSubclassOf(typeof(IDataType))
                          && !type.IsAbstract
                          && type.GetConstructor(Type.EmptyTypes) != null
                    select type;
            }
        };

        public static class Instruction
        {
            private static readonly Dictionary<string, Func<IInstruction>> Registry =
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
            /// Gets all registered instruction names
            /// </summary>
            public static IEnumerable<string> Names => Registry.Keys.ToList();

            /// <summary>
            /// Registers an instruction to the Logix context
            /// </summary>
            /// <param name="name">The name of the instruction to register</param>
            /// <param name="create">A delegate for creating the registered instruction</param>
            /// <exception cref="ArgumentException">When name is null or empty</exception>
            /// <exception cref="ArgumentNullException">When create is null</exception>
            /// <exception cref="ComponentNameCollisionException">When the provided name already exists in the registry</exception>
            public static void Register(string name, Func<IInstruction> create)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Name can not be null or empty");

                if (create == null)
                    throw new ArgumentNullException(nameof(create), "Create delegate can not be null or empty");

                if (Registry.ContainsKey(name))
                    throw new ComponentNameCollisionException(name, typeof(IInstruction));

                Registry.Add(name, create);
            }

            /// <summary>
            /// Determines if the instruction name is registered in the current Logix context
            /// </summary>
            /// <param name="name">The name of the instruction to search</param>
            /// <returns>True when the registry contains the name of the instruction provided</returns>
            public static bool Contains(string name)
            {
                return Registry.ContainsKey(name);
            }

            /// <summary>
            /// Creates an instance of the given instruction
            /// </summary>
            /// <param name="name">The name of the instruction to create</param>
            /// <returns>An instance of the IInstruction if it is registered. An instance of Undefined otherwise</returns>
            public static IInstruction Create(string name)
            {
                return Registry.ContainsKey(name) ? Registry[name].Invoke() : null;
            }
            
        };
    }
}