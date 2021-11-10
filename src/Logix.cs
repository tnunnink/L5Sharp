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
        private static readonly Dictionary<string, Func<IDataType>> DataTypeRegistry =
            new Dictionary<string, Func<IDataType>>(StringComparer.OrdinalIgnoreCase)
            {
                { nameof(Undefined), () => new Undefined() },
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

        private static readonly Dictionary<string, Func<IInstruction>> InstructionRegistry =
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
        /// List of all registered Logix Data Types
        /// </summary>
        public static IEnumerable<string> DataTypes => DataTypeRegistry.Keys.AsEnumerable();

        /// <summary>
        /// List of all registered Logix Instructions
        /// </summary>
        public static IEnumerable<string> Instructions => InstructionRegistry.Keys.AsEnumerable();

        /// <summary>
        /// Registers a data type to the global Logix context
        /// </summary>
        /// <param name="name">The name of the data type to register. The name must be unique (i.e., not the name of a current type</param>
        /// <param name="factory">A factory delegate for creating the registered data type</param>
        /// <exception cref="ArgumentException">When name is null or empty</exception>
        /// <exception cref="ArgumentNullException">When factory is null</exception>
        /// <exception cref="ComponentNameCollisionException">When the provided name already exists in the registry</exception>
        public static void Register(string name, Func<IDataType> factory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty");

            if (factory == null)
                throw new ArgumentNullException(nameof(factory), "Create delegate can not be null or empty");

            if (DataTypeRegistry.ContainsKey(name))
                throw new ComponentNameCollisionException(name, typeof(IDataType));

            DataTypeRegistry.Add(name, factory);
        }

        /// <summary>
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

            if (InstructionRegistry.ContainsKey(name))
                throw new ComponentNameCollisionException(name, typeof(IInstruction));

            InstructionRegistry.Add(name, factory);
        }

        /// <summary>
        /// Determines if the data type name is registered in the current Logix context
        /// </summary>
        /// <param name="name">The name of the data type to search</param>
        /// <returns>True when the registry contains the name of the data type provided</returns>
        public static bool ContainsType(string name)
        {
            return DataTypeRegistry.ContainsKey(name);
        }

        /// <summary>
        /// Determines if the instruction name is registered in the current Logix context
        /// </summary>
        /// <param name="name">The name of the instruction to search</param>
        /// <returns>True when the registry contains the name of the instruction provided</returns>
        public static bool ContainsInstruction(string name)
        {
            return InstructionRegistry.ContainsKey(name);
        }

        /// <summary>
        /// Creates an instance of the given data type
        /// </summary>
        /// <param name="name">The name of the data type to create</param>
        /// <returns>An instance of the IDatatype if it is registered. An instance of Undefined otherwise</returns>
        public static IDataType CreateType(string name)
        {
            return DataTypeRegistry.ContainsKey(name) ? DataTypeRegistry[name].Invoke() : new Undefined();
        }

        public static TDataType Create<TDataType>(string name) where TDataType : class, IDataType
        {
            return DataTypeRegistry.ContainsKey(name) ? DataTypeRegistry[name].Invoke() as TDataType : default;
        } 

        /// <summary>
        /// Creates an instance of the given instruction
        /// </summary>
        /// <param name="name">The name of the instruction to create</param>
        /// <returns>An instance of the IInstruction if it is registered. An instance of Undefined otherwise</returns>
        public static IInstruction CreateInstruction(string name)
        {
            return InstructionRegistry.ContainsKey(name) ? InstructionRegistry[name].Invoke() : null;
        }
        
        

        //todo perhaps a nice feature to automatically register user implementations of IDataType or IInstruction?
        private static IEnumerable<Type> FindConstructableAssemblyImplementations(Type type)
        {
            return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from t in assembly.GetTypes()
                where t.IsSubclassOf(type)
                      && !t.IsAbstract
                      && t.GetConstructor(Type.EmptyTypes) != null
                select t;
        }
    }
}