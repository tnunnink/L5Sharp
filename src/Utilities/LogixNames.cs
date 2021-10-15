using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Utilities
{
    public static class LogixNames
    {
        public static string GetComponentName<T>() where T : IComponent
        {
            var key = FindKey<T>();
            
            if (!Components.ContainsKey(key))
                throw new InvalidOperationException($"No component name defined for type '{key}'");

            return Components[key];
        }

        public static string GetContainerName<T>()
        {
            var key = FindKey<T>();
            
            if (!Containers.ContainsKey(key))
                throw new InvalidOperationException($"No container name defined for type '{key}'");

            return Containers[key];
        }

        private static Type FindKey<T>()
        {
            return Components.Keys.FirstOrDefault(t => t.IsAssignableFrom(typeof(T)));
        }

        private static readonly Dictionary<Type, string> Components = new Dictionary<Type, string>
        {
            { typeof(IDataType), ComponentNames.DataType },
            { typeof(IMember), ComponentNames.Member },
            { typeof(Module), ComponentNames.Module },
            { typeof(ITag), ComponentNames.Tag },
            { typeof(IProgram), ComponentNames.Program },
            { typeof(ITask), ComponentNames.Task }
        };

        private static readonly Dictionary<Type, string> Containers = new Dictionary<Type, string>
        {
            { typeof(LogixContext), ContainerNames.RSLogix5000Content },
            { typeof(IDataType), ContainerNames.DataTypes },
            { typeof(IMember), ContainerNames.Members },
            { typeof(ITag), ContainerNames.Tags },
            { typeof(IProgram), ContainerNames.Programs },
            { typeof(IRoutine), ContainerNames.Routines },
            { typeof(Rung), ContainerNames.Rungs },
            { typeof(ITask), ContainerNames.Tasks }
        };

        private static class ContainerNames
        {
            // ReSharper disable once InconsistentNaming
            public const string RSLogix5000Content = nameof(RSLogix5000Content);
            public const string Controller = nameof(Controller);
            public const string DataTypes = nameof(DataTypes);
            public const string Members = nameof(Members);
            public const string Modules = nameof(Modules);
            public const string AddOnInstructions = nameof(AddOnInstructions);
            public const string Tags = nameof(Tags);
            public const string Programs = nameof(Programs);
            public const string Routines = nameof(Routines);
            public const string Rungs = nameof(Rungs);
            public const string Tasks = nameof(Tasks);
        }
        
        private static class ComponentNames
        {
            public const string Controller = nameof(Controller);
            public const string DataType = nameof(DataType);
            public const string Member = nameof(Member);
            public const string Module = nameof(Module);
            public const string AddOnInstructionDefinition = nameof(AddOnInstructionDefinition);
            public const string Tag = nameof(Tag);
            public const string Program = nameof(Program);
            public const string Routine = nameof(Routine);
            public const string Task = nameof(Task);   
        }
    }
}