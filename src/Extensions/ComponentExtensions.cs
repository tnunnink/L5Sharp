using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Extensions
{
    /// <summary>
    /// A class of extension for <see cref="ILogixComponent"/> implementations
    /// </summary>
    internal static class ComponentExtensions
    {
        private static readonly Dictionary<Type, IL5XSerializer> Serializers = new()
        {
            { typeof(IController), new ControllerSerializer() },
            { typeof(IUserDefined), new UserDefinedSerializer() },
            { typeof(IMember<IDataType>), new MemberSerializer() },
            { typeof(IModule), new ModuleSerializer() },
            { typeof(ITag<IDataType>), new TagSerializer() },
            { typeof(IProgram), new ProgramSerializer() },
            { typeof(IRoutine<ILogixContent>), new RoutineSerializer() },
            { typeof(ITask), new TaskSerializer() }
        };


        /// <summary>
        /// Serializes an <see cref="ILogixComponent"/> instance to a <see cref="XElement"/> using the serializer
        /// defined for the component type.
        /// </summary>
        /// <param name="component">The <see cref="ILogixComponent"/> instance to serialize.</param>
        /// <typeparam name="TComponent">The type of logix component to serialize.</typeparam>
        /// <returns>A new <see cref="XElement"/> instance that represents the serialized L5X of the component.</returns>
        public static XElement Serialize<TComponent>(this TComponent component)
            where TComponent : ILogixComponent => GetSerializer<TComponent>().Serialize(component);

        /// <summary>
        /// Deserialized an <see cref="XElement"/> instance to a <see cref="ILogixComponent"/> using the serializer
        /// defined for the component type.
        /// </summary>
        /// <param name="element">The <see cref="XElement"/> instance to deserialize.</param>
        /// <typeparam name="TComponent">The type of logix component to deserialize.</typeparam>
        /// <returns>A new <see cref="ILogixComponent"/> type that represents the deserialized component of the current element.</returns>
        public static TComponent Deserialize<TComponent>(this XElement element)
            where TComponent : ILogixComponent => GetSerializer<TComponent>().Deserialize(element);

        private static IL5XSerializer<TComponent> GetSerializer<TComponent>()
            where TComponent : ILogixComponent
        {
            var serializer = Serializers.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;

            if (serializer is null)
                throw new InvalidOperationException($"No serializer defined for'{typeof(TComponent)}'");

            return (IL5XSerializer<TComponent>)serializer;
        }
    }
}