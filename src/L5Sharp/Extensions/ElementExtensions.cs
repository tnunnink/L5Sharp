using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;
using Module = L5Sharp.Core.Module;

[assembly: InternalsVisibleTo("L5Sharp.Tests")]

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
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

        public static T Deserialize<T>(this XElement element, LogixContext context) where T : IComponent
        {
            /*var type = typeof(T);

            if (!Serializers.ContainsKey(type))
                throw new InvalidOperationException($"No serializer defined for type '{type}'");

            var serializer = (IComponentSerializer<T>)Serializers[type];*/
            
            var serializer = context.GetSerializer<T>();

            return default;
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
        
        public static string GetName(this XElement element) =>
            element.GetAttributeValueInternal<IComponent, string, string>(c => c.Name, s => s);
        
        public static string GetDescription(this XElement element) =>
            element.GetElementValueInternal<IComponent, string, string>(c => c.Description, s => s);

        public static TReturn GetValue<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, parse);

        public static TProperty GetValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TProperty> parse)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, parse);

        public static string GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, string>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, s => s);

        public static bool GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, bool>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToBoolean);
        
        public static ushort GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, ushort>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToUInt16);

        public static short GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, short>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToInt16);
        
        public static uint GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, uint>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToUInt32);

        public static int GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, int>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToInt32);
        
        public static float GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, float>> propertyExpression)
            where TComponent : IComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToSingle);
        
        public static DataTypeFamily GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeFamily>> propertyExpression)
            where TComponent : IComponent => 
            element.GetAttributeValueInternal(propertyExpression, v => v != null ? DataTypeFamily.FromName(v) : null);
        
        public static DataTypeClass GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeClass>> propertyExpression)
            where TComponent : IComponent => 
            element.GetAttributeValueInternal(propertyExpression, v => v != null ? DataTypeClass.FromName(v) : null);
        
        public static Radix GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, Radix>> propertyExpression)
            where TComponent : IComponent 
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? Radix.FromName(v) : null);

        public static ExternalAccess GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, ExternalAccess>> propertyExpression)
            where TComponent : IComponent
            => element.GetAttributeValueInternal(propertyExpression,
                v => v != null ? ExternalAccess.FromName(v) : null);

        public static string GetElementValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression)
            where TComponent : IComponent => element.GetElementValueInternal(propertyExpression, s => s);

        public static TProperty GetElementValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TProperty> parse)
            where TComponent : IComponent
        {
            var componentName = GetComponentName<TComponent>();
            
            if (element.Name != componentName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{componentName}'");
            
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"Expression must of type {typeof(MemberExpression)}");

            var memberName = memberExpression.Member.Name;
            var value = element.Element(memberName)?.Value;
            
            return parse(value);
        }

        private static TReturn GetAttributeValueInternal<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
            where TComponent : IComponent
        {
            var componentName = GetComponentName<TComponent>();
            
            if (element.Name != componentName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{componentName}'");
            
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"Expression must of type {typeof(MemberExpression)}");

            var attributeName = memberExpression.Member.Name;
            var value = element.Attribute(attributeName)?.Value;
            
            return parse(value);
        }

        private static TReturn GetElementValueInternal<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
            where TComponent : IComponent
        {
            var componentName = GetComponentName<TComponent>();
            
            if (element.Name != componentName)
                throw new InvalidOperationException(
                    $"Element name '{element.Name}' is not expected value '{componentName}'");
            
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"Expression must of type {typeof(MemberExpression)}");

            var memberName = memberExpression.Member.Name;
            var value = element.Element(memberName)?.Value;
            
            return parse(value);
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