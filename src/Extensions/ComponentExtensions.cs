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
    public static class ComponentExtensions
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
        /// Creates a new <see cref="L5XContext"/> with the current <see cref="ILogixComponent"/> as the target component.
        /// </summary>
        /// <param name="component">The current logix component for which to create the new <see cref="L5XContext"/>.</param>
        /// <typeparam name="TComponent">The logix component type of the current instance.</typeparam>
        /// <returns>A new <see cref="L5XContext"/> instance with the current component as the target of the context.</returns>
        /// <remarks>
        /// This 
        /// </remarks>
        public static L5XContext Export<TComponent>(this TComponent component)
            where TComponent : ILogixComponent => L5XContext.Create(component);

        /// <summary>
        /// Serializes an <see cref="ILogixComponent"/> instance to a <see cref="XElement"/> using the serializer
        /// defined for the component type.
        /// </summary>
        /// <param name="component">The <see cref="ILogixComponent"/> instance to serialize.</param>
        /// <typeparam name="TComponent">The type of logix component to serialize.</typeparam>
        /// <returns>A new <see cref="XElement"/> instance that represents the serialized L5X of the component.</returns>
        internal static XElement Serialize<TComponent>(this TComponent component)
            where TComponent : ILogixComponent
        {
            var serializer = Serializers.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;

            if (serializer is null)
                throw new InvalidOperationException($"No serializer defined for'{typeof(TComponent)}'");

            return ((IL5XSerializer<TComponent>)serializer).Serialize(component);
        }
    }
}