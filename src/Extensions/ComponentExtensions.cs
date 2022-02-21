using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
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
            { typeof(IMember<>), new MemberSerializer() },
            { typeof(Module), new ModuleSerializer() },
            { typeof(ITag<>), new TagSerializer() },
            { typeof(IProgram), new ProgramSerializer() },
            { typeof(IRoutine<>), new RoutineSerializer() },
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
            where TComponent : ILogixComponent => Serializers.TryGetValue(typeof(TComponent), out var serializer)
            ? (IL5XSerializer<TComponent>)serializer
            : throw new InvalidOperationException($"No serializer defined for'{typeof(TComponent)}'");
    }
}