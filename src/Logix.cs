using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using L5Sharp.Core;
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
            private static readonly Dictionary<string, IDataType> Registry =
                new Dictionary<string, IDataType>(StringComparer.OrdinalIgnoreCase);
            
            public static readonly Undefined Undefined = new Undefined();
            public static readonly Bool Bit = new Bool();
            public static readonly Bool Bool = new Bool();
            public static readonly Sint Sint = new Sint();
            public static readonly Int Int = new Int();
            public static readonly Dint Dint = new Dint();
            public static readonly Lint Lint = new Lint();
            public static readonly Real Real = new Real();
            public static readonly String String = new String();
            public static readonly Timer Timer = new Timer();
            public static readonly Counter Counter = new Counter();
            public static readonly Alarm Alarm = new Alarm();
            
            public static IEnumerable<IDataType> All => Registry.Values.ToList();
            public static IEnumerable<IDataType> Atomics => Registry.Values.Where(t => t is IAtomic).ToList();

            public static void Register(IDataType type)
            {
                if (type == null)
                    throw new ArgumentNullException(nameof(type), "Type can not be null");
                    
                if (Registry.ContainsKey(type.Name))
                    throw new ComponentNameCollisionException(type.Name, type.GetType());
                
                Registry.Add(type.Name, type);
            }
            
            public static bool Contains(string name)
            {
                return Registry.ContainsKey(name)
                       || FindFieldType(name) != null
                       || FindAssemblyType(name) != null;
            }

            public static IDataType Parse(string name)
            {
                if (Registry.ContainsKey(name))
                    return Registry[name];

                var fieldType = FindFieldType(name);
                if (fieldType != null)
                    return fieldType;

                var assemblyType = FindAssemblyType(name);

                try
                {
                    return (IDataType)Activator.CreateInstance(assemblyType);
                }
                catch (Exception)
                {
                    return Undefined;
                }
            }
            
            private static IDataType FindFieldType(string name)
            {
                var field = typeof(DataType).GetField(name, BindingFlags.Public
                                                              | BindingFlags.Static
                                                              | BindingFlags.IgnoreCase);

                return (IDataType)field?.GetValue(null);
            }
            
            private static Type FindAssemblyType(string name)
            {
                return GetAssemblyTypes().SingleOrDefault(t => t.Name == name);
            }
            
            private static IEnumerable<Type> GetAssemblyTypes()
            {
                return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.IsSubclassOf(typeof(Predefined))
                          && !type.IsAbstract
                          && type.GetConstructor(Type.EmptyTypes) != null
                    select type;
            }
        };
        
        public static class Instruction
        {
            private static readonly Dictionary<string, IInstruction> Registry =
                new Dictionary<string, IInstruction>(StringComparer.OrdinalIgnoreCase);
            
            public static readonly XIC NULL = new XIC();
            public static readonly XIC XIC = new XIC();
            public static readonly XIC XIO = new XIC();
            public static readonly XIC OTE = new XIC();
            public static readonly XIC OTL = new XIC();
            public static readonly XIC OTU = new XIC();
            public static readonly XIC ONS = new XIC();
            public static readonly XIC OSR = new XIC();
            public static readonly XIC OSF = new XIC();
            public static readonly XIC OSRI = new XIC();
            public static readonly XIC OSFI = new XIC();
            public static readonly MOV MOV = new MOV();
            
            public static void Register(IInstruction instruction)
            {
                if (instruction == null)
                    throw new ArgumentNullException(nameof(instruction), "Instruction can not be null");
                    
                if (Registry.ContainsKey(instruction.Name))
                    throw new ComponentNameCollisionException(instruction.Name, instruction.GetType());
                
                Registry.Add(instruction.Name, instruction);
            }
            
            public static bool Contain(string name)
            {
                return Registry.ContainsKey(name)
                       || FindFieldInstruction(name) != null
                       || FindAssemblyInstruction(name) != null;
            }

            public static IInstruction Parse(string name)
            {
                if (Registry.ContainsKey(name))
                    return Registry[name];

                var fieldType = FindFieldInstruction(name);
                if (fieldType != null)
                    return fieldType;

                var assemblyType = FindAssemblyInstruction(name);

                try
                {
                    return (IInstruction)Activator.CreateInstance(assemblyType);
                }
                catch (Exception)
                {
                    return NULL;
                }
            }
            
            private static IInstruction FindFieldInstruction(string name)
            {
                var field = typeof(Instruction).GetField(name, BindingFlags.Public
                                                              | BindingFlags.Static
                                                              | BindingFlags.IgnoreCase);

                return (IInstruction)field?.GetValue(null);
            }
            
            private static Type FindAssemblyInstruction(string name)
            {
                return GetAssemblyTypes().SingleOrDefault(t => t.Name == name);
            }
            
            private static IEnumerable<Type> GetAssemblyTypes()
            {
                return from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    from type in assembly.GetTypes()
                    where type.IsSubclassOf(typeof(L5Sharp.Core.Instruction))
                          && !type.IsAbstract
                          && type.GetConstructor(Type.EmptyTypes) != null
                    select type;
            }
        };
    }
}