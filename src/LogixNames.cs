namespace L5Sharp
{
    public static class LogixNames
    {
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
        public const string Tags = nameof(Tags);
        public const string Tag = nameof(Tag);
        public const string Programs = nameof(Programs);
        public const string Program = nameof(Program);
        public const string Routines = nameof(Routines);
        public const string Routine = nameof(Routine);
        public const string Rungs = nameof(Rungs);
        public const string Tasks = nameof(Tasks);
        public const string Task = nameof(Task);

        /*public static string GetComponentName<T>() where T : ILogixComponent
        {
            var key = FindKey<T>();
            
            if (!Components.ContainsKey(key))
                throw new InvalidOperationException($"No component name defined for type '{key}'");

            return Components[key];
        }

        public static string GetContainerName<T>() where T : ILogixComponent
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
            { typeof(IUserDefined), ComponentNames.DataType },
            { typeof(IMember<IDataType>), ComponentNames.Member },
            { typeof(Module), ComponentNames.Module },
            { typeof(IAddOnDefined), ComponentNames.AddOnInstructionDefinition },
            { typeof(ITag<IDataType>), ComponentNames.Tag },
            { typeof(IProgram), ComponentNames.Program },
            { typeof(ITask), ComponentNames.Task }
        };

        private static readonly Dictionary<Type, string> Containers = new Dictionary<Type, string>
        {
            { typeof(IUserDefined), ContainerNames.DataTypes },
            { typeof(IMember<IDataType>), ContainerNames.Members },
            { typeof(IAddOnDefined), ComponentNames.AddOnInstructionDefinition },
            { typeof(ITag<IDataType>), ContainerNames.Tags },
            { typeof(IProgram), ContainerNames.Programs },
            { typeof(IRoutine), ContainerNames.Routines },
            { typeof(Rung), ContainerNames.Rungs },
            { typeof(ITask), ContainerNames.Tasks }
        };*/

        /*public static class ContainerNames
        {
            public const string Controller = nameof(Controller);
            public const string DataTypes = nameof(DataTypes);
            public const string Members = nameof(Members);
            public const string Modules = nameof(Modules);
            public const string AddOnInstructionDefinitions = nameof(AddOnInstructionDefinitions);
            public const string Tags = nameof(Tags);
            public const string Programs = nameof(Programs);
            public const string Routines = nameof(Routines);
            public const string Rungs = nameof(Rungs);
            public const string Tasks = nameof(Tasks);
        }*/
        
        /*private static class ComponentNames
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
        }*/
    }
}