using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class L5X : XDocument
    {
        private static readonly Dictionary<Type, string> Components = new Dictionary<Type, string>
        {
            { typeof(IDataType), LogixNames.Components.DataType },
            { typeof(DataType), LogixNames.Components.DataType },
            { typeof(IMember), LogixNames.Components.Member },
            { typeof(Member), LogixNames.Components.Member },
            { typeof(Module), LogixNames.Components.Module },
            //{ typeof(Instruction), L5XNames.Components.AddOnInstructionDefinition },
            { typeof(Tag), LogixNames.Components.Tag },
            { typeof(Program), LogixNames.Components.Program },
            { typeof(PeriodicTask), LogixNames.Components.Task }
        };

        private static readonly Dictionary<Type, string> Containers = new Dictionary<Type, string>
        {
            { typeof(IDataType), LogixNames.Containers.DataTypes },
            { typeof(IMember), LogixNames.Containers.Modules },
            //{ typeof(Instruction), L5XNames.Containers.AddOnInstructions },
            { typeof(Tag), LogixNames.Containers.Tags },
            { typeof(Program), LogixNames.Containers.Programs },
            { typeof(IRoutine), LogixNames.Containers.Routines },
            { typeof(ITask), LogixNames.Containers.Tasks }
        };
        
        public XElement Content => Root;
        public IEnumerable<XElement> DataTypes => Descendants(LogixNames.Components.DataType);
        public IEnumerable<XElement> Programs => Descendants(LogixNames.Components.Program);
        public IEnumerable<XElement> Tasks => Descendants(LogixNames.Components.Task);

        public XElement FindDataType(string name)
        {
            return Descendants(LogixNames.Components.DataType).SingleOrDefault(x => x.GetName() == name);
        }
        
        public bool Contains<T>(string name) where T : IComponent
        {
            var component = ComponentName<T>();
            return Content.Descendants(component).FirstOrDefault(x => x.GetName() == name) != null;
        }
        
        public IEnumerable<XElement> GetAll<T>() where T : IComponent
        {
            var component = ComponentName<T>();
            return Content.Descendants(component);
        }

        public XElement GetFirst<T>(string name) where T : IComponent
        {
            var component = ComponentName<T>();
            return Content.Descendants(component).FirstOrDefault(x => x.GetName() == name);
        }

        public XElement GetSingle<T>(string name) where T : IComponent
        {
            var component = ComponentName<T>();
            return Content.Descendants(component).SingleOrDefault(x => x.GetName() == name);
        }

        public XElement Container<T>() where T : IComponent
        {
            var container = ContainerName<T>();
            return Content.Descendants(container).FirstOrDefault();
        }

        public string ComponentName<T>() where T : IComponent
        {
            if (!Components.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Component name not defined for type '{typeof(T)}'");

            return Components[typeof(T)];
        }
        
        public string ContainerName<T>() where T : IComponent
        {
            if (!Containers.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Container name not defined for type '{typeof(T)}'");

            return Containers[typeof(T)];
        }
    }
}