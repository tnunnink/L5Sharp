using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Common
{
    /// <summary>
    /// Helper class that contains all Logix L5X component and attribute XName values as strongly typed members so to
    /// avoid using magic strings and allow us to update them in a central location as necessary.
    /// </summary>
    internal static class LogixNames
    {
        //Component names
        public static XName RsLogix5000Content = XName.Get(nameof(RsLogix5000Content));
        public static XName Controller = XName.Get(nameof(Controller));
        public static XName DataTypes = XName.Get(nameof(DataTypes));
        public static XName DataType = XName.Get(nameof(DataType));
        public static XName Members = XName.Get(nameof(Members));
        public static XName Member = XName.Get(nameof(Member));
        public static XName Modules = XName.Get(nameof(Modules));
        public static XName Module = XName.Get(nameof(Module));
        public static XName AddOnInstructionDefinitions = XName.Get(nameof(AddOnInstructionDefinitions));
        public static XName AddOnInstructionDefinition = XName.Get(nameof(AddOnInstructionDefinition));
        public static XName Parameters = XName.Get(nameof(Parameters));
        public static XName Parameter = XName.Get(nameof(Parameter));
        public static XName Tags = XName.Get(nameof(Tags));
        public static XName Tag = XName.Get(nameof(Tag));
        public static XName Programs = XName.Get(nameof(Programs));
        public static XName Program = XName.Get(nameof(Program));
        public static XName Routines = XName.Get(nameof(Routines));
        public static XName Routine = XName.Get(nameof(Routine));
        public static XName Rungs = XName.Get(nameof(Rungs));
        public static XName Rung = XName.Get(nameof(Rung));
        public static XName Tasks = XName.Get(nameof(Tasks));
        public static XName Task = XName.Get(nameof(Task));
        //Logix data structure names
        public static XName Data = XName.Get(nameof(Data));
        public static XName Value = XName.Get(nameof(Value));
        public static XName DataValue = XName.Get(nameof(DataValue));
        public static XName Array = XName.Get(nameof(Array));
        public static XName Index = XName.Get(nameof(Index));
        public static XName Element = XName.Get(nameof(Element));
        public static XName Structure = XName.Get(nameof(Structure));
        public static XName ArrayMember = XName.Get(nameof(ArrayMember));
        public static XName DataValueMember = XName.Get(nameof(DataValueMember));
        public static XName StructureMember = XName.Get(nameof(StructureMember));
        //Attribute Names
        public static XName Name = XName.Get(nameof(Name));
        public static XName Description = XName.Get(nameof(Description));
        public static XName Dimension = XName.Get(nameof(Dimension));
        public static XName Dimensions = XName.Get(nameof(Dimensions));
        public static XName Radix = XName.Get(nameof(Radix));
        public static XName ExternalAccess = XName.Get(nameof(ExternalAccess));
        


        private static readonly Dictionary<Type, Tuple<XName, XName>> ComponentNameMap = new()
        {
            { typeof(IController), new Tuple<XName, XName>(Controller, string.Empty) },
            { typeof(IUserDefined), new Tuple<XName, XName>(DataType, DataTypes) },
            { typeof(IMember<IDataType>), new Tuple<XName, XName>(Member, Members) },
            { typeof(IModule), new Tuple<XName, XName>(Module, Modules) },
            {
                typeof(IAddOnInstruction),
                new Tuple<XName, XName>(AddOnInstructionDefinition, AddOnInstructionDefinitions)
            },
            { typeof(IParameter<IDataType>), new Tuple<XName, XName>(Parameter, Parameters) },
            { typeof(ITag<IDataType>), new Tuple<XName, XName>(Tag, Tags) },
            { typeof(IProgram), new Tuple<XName, XName>(Program, Programs) },
            { typeof(IRoutine), new Tuple<XName, XName>(Routine, Routines) },
            { typeof(IRung), new Tuple<XName, XName>(Rung, Rungs) },
            { typeof(ITask), new Tuple<XName, XName>(Task, Tasks) }
        };

        /// <summary>
        /// A helper for dynamically getting a component/L5X <c>XName</c> based on the object type. 
        /// </summary>
        /// <typeparam name="TComponent">The type of the <c>ILogixComponent</c> to get a XName for.</typeparam>
        /// <returns>A <c>XName</c> that corresponds to the L5X element name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified <see cref="TComponent"/> has not mapping defined.</exception>
        public static XName GetComponentName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component name mapping defined for type '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item1;
        }

        /// <summary>
        /// A helper for dynamically getting a component/L5X <c>XName</c> based on the object type. 
        /// </summary>
        /// <typeparam name="TComponent">The type of the <c>ILogixComponent</c> to get a XName for.</typeparam>
        /// <returns>A <c>XName</c> that corresponds to the L5X component's container name for the type of component specified.</returns>
        /// <exception cref="InvalidOperationException">When the specified <see cref="TComponent"/> has not mapping defined.</exception>
        public static XName GetContainerName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException(
                    $"No component name mapping defined for type '{typeof(TComponent)}'");

            return ComponentNameMap[key].Item2;
        }
        
        private static Type? FindKey<TComponent>()
        {
            return ComponentNameMap.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(TComponent)));
        }
    }
}