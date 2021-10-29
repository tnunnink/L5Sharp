using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Serialization;

namespace L5Sharp.Extensions
{
    public static class GenericExtensions
    {
        private static readonly Dictionary<Type, IComponentSerializer> Serializers = new Dictionary<Type, IComponentSerializer>
        {
            { typeof(IDataType), new DataTypeSerializer() },
            { typeof(DataType), new DataTypeSerializer() },
            { typeof(IMember), new MemberSerializer() },
            { typeof(DataTypeMember), new MemberSerializer() },
            { typeof(ITag<IDataType>), new TagSerializer() },
            { typeof(IProgram), new ProgramSerializer() },
            { typeof(ITask), new TaskSerializer() }
        };
        
        public static XElement Serialize<T>(this T component) where T : IComponent
        {
            var type = typeof(T);

            if (!Serializers.ContainsKey(type))
                throw new InvalidOperationException($"Serializer not defined for component of type '{type}'");

            var serializer = (IComponentSerializer<T>)Serializers[type];

            return serializer.Serialize(component);
        }
        
        public static XAttribute ToAttribute<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> propertyExpression)
            where TComponent : IComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            var func = propertyExpression.Compile();
            var value = func(component);

            return new XAttribute(memberExpression.Member.Name, value);
        }

        public static XElement ToElement<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> propertyExpression, bool useCDataElement = true)
            where TComponent : IComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException();

            var func = propertyExpression.Compile();
            var value = func(component);

            return useCDataElement
                ? new XElement(memberExpression.Member.Name, new XCData(value.ToString()))
                : new XElement(memberExpression.Member.Name, value);
        }
    }
}