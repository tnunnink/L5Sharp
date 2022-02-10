using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Helpers
{
    /// <summary>
    /// Helper class that contains all Logix L5X component and attribute XName values as strongly typed members so to
    /// avoid using magic strings and allow us to update them in a central location as necessary.
    /// </summary>
    internal static class LogixNames
    {
        //Component names
        public static string RsLogix5000Content = nameof(RsLogix5000Content);
        public static string Controller = nameof(Controller);
        public static string DataTypes = nameof(DataTypes);
        public static string DataType = nameof(DataType);
        public static string Members = nameof(Members);
        public static string Member = nameof(Member);
        public static string Modules = nameof(Modules);
        public static string Module = nameof(Module);
        public static string Ports = nameof(Ports);
        public static string Port = nameof(Port);
        public static string Bus = nameof(Bus); 
        public static string Connections = nameof(Connections);
        public static string Connection = nameof(Connection);
        public static string AddOnInstructionDefinitions = nameof(AddOnInstructionDefinitions);
        public static string AddOnInstructionDefinition = nameof(AddOnInstructionDefinition);
        public static string Parameters = nameof(Parameters);
        public static string Parameter = nameof(Parameter);
        public static string Tags = nameof(Tags);
        public static string Tag = nameof(Tag);
        public static string Programs = nameof(Programs);
        public static string Program = nameof(Program);
        public static string Routines = nameof(Routines);
        public static string Routine = nameof(Routine);
        public static string RllContent = nameof(RllContent);
        public static string StContent = nameof(StContent);
        public static string Rungs = nameof(Rungs);
        public static string Rung = nameof(Rung);
        public static string Tasks = nameof(Tasks);
        public static string Task = nameof(Task);

        //Logix data structure names
        public static string Data = nameof(Data);
        public static string Value = nameof(Value);
        public static string DataValue = nameof(DataValue);
        public static string Array = nameof(Array);
        public static string Index = nameof(Index);
        public static string Element = nameof(Element);
        public static string Structure = nameof(Structure);
        public static string ArrayMember = nameof(ArrayMember);
        public static string DataValueMember = nameof(DataValueMember);
        public static string StructureMember = nameof(StructureMember);

        //Attribute Names
        public static string Name = nameof(Name);
        public static string Description = nameof(Description);

        //Other..
        public static string EventInfo = nameof(EventInfo);
        public static string Comment = nameof(Comment);
        public static string Comments = nameof(Comments);
        public static string InputTag = nameof(InputTag);
        public static string OutputTag = nameof(OutputTag);
        

        //Component Map Collection


        private static readonly Dictionary<Type, Tuple<string, string>> ComponentNameMap = new()
        {
            { typeof(IController), new Tuple<string, string>(Controller, string.Empty) },
            { typeof(IUserDefined), new Tuple<string, string>(DataType, DataTypes) },
            { typeof(IMember<IDataType>), new Tuple<string, string>(Member, Members) },
            { typeof(Module), new Tuple<string, string>(Module, Modules) },
            {
                typeof(IAddOnInstruction),
                new Tuple<string, string>(AddOnInstructionDefinition, AddOnInstructionDefinitions)
            },
            { typeof(Port), new Tuple<string, string>(Port, Ports) },
            { typeof(Connection), new Tuple<string, string>(Connection, Connections) },
            { typeof(IParameter<IDataType>), new Tuple<string, string>(Parameter, Parameters) },
            { typeof(ITag<IDataType>), new Tuple<string, string>(Tag, Tags) },
            { typeof(IProgram), new Tuple<string, string>(Program, Programs) },
            { typeof(IRoutine<ILogixContent>), new Tuple<string, string>(Routine, Routines) },
            { typeof(ILadderLogic), new Tuple<string, string>(RllContent, string.Empty) },
            { typeof(IStructuredText), new Tuple<string, string>(StContent, string.Empty) },
            { typeof(Rung), new Tuple<string, string>(Rung, Rungs) },
            { typeof(ITask), new Tuple<string, string>(Task, Tasks) }
        };

        

        /// <summary>
        /// A helper for dynamically getting a component <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified <see cref="TComponent"/> has no mapping defined.</exception>
        public static string GetComponentName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component type to name mapping defined for type '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item1;
        }

        /// <summary>
        /// A helper for dynamically getting a container <see cref="XName"/> based on the specified object type. 
        /// </summary>
        /// <typeparam name="TComponent">The component type to get an XName for.</typeparam>
        /// <returns>A <see cref="string"/> that represents the L5X container element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified <see cref="TComponent"/> has no mapping defined.</exception>
        public static string GetContainerName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component name mapping defined for type '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item2;
        }

        private static Type? FindKey<TComponent>() =>
            ComponentNameMap.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(TComponent)));
    }
}