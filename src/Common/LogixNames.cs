using System;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Common
{
    internal static class LogixNames
    {
        //Logix Component names
        public const string RsLogix5000Content = nameof(RsLogix5000Content);
        public const string Controller = nameof(Controller);
        public const string DataTypes = nameof(DataTypes);
        public const string DataType = nameof(DataType);
        public const string Members = nameof(Members);
        public const string Member = nameof(Member);
        public const string Modules = nameof(Modules);
        public const string Module = nameof(Module);
        public const string AddOnInstructionDefinitions = nameof(AddOnInstructionDefinitions);
        public const string AddOnInstructionDefinition = nameof(AddOnInstructionDefinition);
        public const string Parameters = nameof(Parameters);
        public const string Parameter = nameof(Parameter);
        public const string Tags = nameof(Tags);
        public const string Tag = nameof(Tag);
        public const string Programs = nameof(Programs);
        public const string Program = nameof(Program);
        public const string Routines = nameof(Routines);
        public const string Routine = nameof(Routine);
        public const string Rungs = nameof(Rungs);
        public const string Rung = nameof(Rung);
        public const string Tasks = nameof(Tasks);
        public const string Task = nameof(Task);
        //Logix data structure names
        public const string Data = nameof(Data);
        public const string Value = nameof(Value);
        public const string DataValue = nameof(DataValue);
        public const string Array = nameof(Array);
        public const string Index = nameof(Index);
        public const string Element = nameof(Element);
        public const string Structure = nameof(Structure);
        public const string ArrayMember = nameof(ArrayMember);
        public const string DataValueMember = nameof(DataValueMember);
        public const string StructureMember = nameof(StructureMember);

        public static string GetComponentName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException($"No component key defined for type '{typeof(TComponent)}'");

            return NameMap[key].Item1;
        }

        public static string GetContainerName<TComponent>()
        {
            var key = FindKey<TComponent>();

            if (key == null)
                throw new InvalidOperationException($"No component key defined for type '{typeof(TComponent)}'");

            return NameMap[key].Item2;
        }

        private static Type? FindKey<TComponent>()
        {
            return NameMap.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(TComponent)));
        }

        private static readonly Dictionary<Type, Tuple<string, string>> NameMap = new()
            {
                { typeof(IController), new Tuple<string, string>(Controller, string.Empty) },
                { typeof(IUserDefined), new Tuple<string, string>(DataType, DataTypes) },
                { typeof(IMember<IDataType>), new Tuple<string, string>(Member, Members) },
                { typeof(IModule), new Tuple<string, string>(Module, Modules) },
                {
                    typeof(IAddOnInstruction),
                    new Tuple<string, string>(AddOnInstructionDefinition, AddOnInstructionDefinitions)
                },
                { typeof(IParameter<IDataType>), new Tuple<string, string>(Parameter, Parameters) },
                { typeof(ITag<IDataType>), new Tuple<string, string>(Tag, Tags) },
                { typeof(IProgram), new Tuple<string, string>(Program, Programs) },
                { typeof(IRoutine), new Tuple<string, string>(Routine, Routines) },
                { typeof(IRung), new Tuple<string, string>(Rung, Rungs) },
                { typeof(ITask), new Tuple<string, string>(Task, Tasks) }
            };
    }
}