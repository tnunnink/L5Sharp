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
            { typeof(IComplexType), new DataTypeSerializer() },
            { typeof(IModule), new ModuleSerializer() },
            { typeof(IAddOnInstruction), new AddOnInstructionSerializer() },
            { typeof(ITag<IDataType>), new TagSerializer() },
            { typeof(IProgram), new ProgramSerializer() },
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
        /// Serializes the current component instance to an <see cref="XElement"/> object.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> type to serialize.</typeparam>
        /// <returns>A new <see cref="XElement"/> object that represents the serialized component.</returns>
        /// <exception cref="InvalidOperationException">No serializer is defined for component.</exception>
        public static XElement Serialize<TComponent>(this TComponent component)
            where TComponent : ILogixComponent
        {
            var serializer = Serializers.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;

            if (serializer is not IL5XSerializer<TComponent> type)
                throw new InvalidOperationException($"No serializer defined for'{typeof(TComponent)}'");

            return type.Serialize(component);
        }
    }
}