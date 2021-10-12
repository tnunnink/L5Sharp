using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Serialization;
using L5Sharp.Utilities;
using Module = L5Sharp.Core.Module;

[assembly: InternalsVisibleTo("L5Sharp.Tests")]

namespace L5Sharp.Extensions
{
    internal static class L5XGenericExtensions
    {
        private static readonly Dictionary<Type, IComponentSerializer> Serializers = new Dictionary<Type, IComponentSerializer>
        {
            { typeof(IDataType), new DataTypeSerializer() },
            { typeof(IMember), new MemberSerializer() },
            { typeof(Tag), new TagSerializer() },
            { typeof(Program), new ProgramSerializer() },
            { typeof(ITask), new TaskSerializer() }
        };

        private static readonly Dictionary<Type, string> Components = new Dictionary<Type, string>
        {
            { typeof(IDataType), LogixNames.Components.DataType },
            { typeof(IMember), LogixNames.Components.Member },
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


        public static XElement Serialize<T>(this T component) where T : IComponent
        {
            var type = typeof(T);

            if (!Serializers.ContainsKey(type))
                throw new InvalidOperationException($"No serializer defined for type '{type}'");

            var serializer = (IComponentSerializer<T>)Serializers[type];

            return serializer.Serialize(component);
        }

        public static T Deserialize<T>(this XElement element) where T : IComponent
        {
            var type = typeof(T);

            if (!Serializers.ContainsKey(type))
                throw new InvalidOperationException($"No serializer defined for type '{type}'");

            var serializer = (IComponentSerializer<T>)Serializers[type];

            return serializer.Deserialize(element);
        }
        
        public static bool Contains<T>(this XElement element, string name) where T : IComponent
        {
            var component = GetComponentName<T>();
            return element.Descendants(component).FirstOrDefault(x => x.GetName() == name) != null;
        }
        
        public static IEnumerable<XElement> GetAll<T>(this XElement element) where T : IComponent
        {
            var component = GetComponentName<T>();
            return element.Descendants(component);
        }

        public static XElement GetFirst<T>(this XElement element, string name) where T : IComponent
        {
            var component = GetComponentName<T>();
            return element.Descendants(component).FirstOrDefault(x => x.GetName() == name);
        }

        public static XElement GetSingle<T>(this XElement element, string name) where T : IComponent
        {
            var component = GetComponentName<T>();
            return element.Descendants(component).SingleOrDefault(x => x.GetName() == name);
        }
        
        public static XElement Container<T>(this XElement element) where T : IComponent
        {
            var container = GetContainerName<T>();
            return element.Descendants(container).FirstOrDefault();
        }

        public static XAttribute ToXAttribute<TComponent, TProperty>(this TComponent component, 
            Expression<Func<TComponent, TProperty>> propertyExpression)
            where TComponent : IComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            var func = propertyExpression.Compile();
            var value = func(component);

            return new XAttribute(memberExpression.Member.Name, value);
        }
        
        public static XElement ToXElement<TComponent, TProperty>(this TComponent component, 
            Expression<Func<TComponent, TProperty>> propertyExpression)
            where TComponent : IComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            var func = propertyExpression.Compile();
            var value = func(component);

            return new XElement(memberExpression.Member.Name, value);
        }
        
        // ReSharper disable once InconsistentNaming
        public static XElement ToXCDataElement<TComponent, TProperty>(this TComponent component, 
            Expression<Func<TComponent, TProperty>> propertyExpression)
            where TComponent : IComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            var func = propertyExpression.Compile();
            var value = func(component);

            return new XElement(memberExpression.Member.Name, new XCData(value.ToString()));
        }
        
        private static string GetComponentName<T>() where T : IComponent
        {
            if (!Components.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"No component name defined for type '{typeof(T)}'");

            return Components[typeof(T)];
        }
        
        private static string GetContainerName<T>() where T : IComponent
        {
            if (!Containers.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"No container name defined for type '{typeof(T)}'");

            return Containers[typeof(T)];
        }
    }
}