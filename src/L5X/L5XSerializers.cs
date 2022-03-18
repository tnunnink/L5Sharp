using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Components;

namespace L5Sharp.L5X
{
    /// <summary>
    /// A class that contains instances of <see cref="IL5XSerializer{T}"/>, which make available the
    /// current <see cref="L5XContext"/> object, so that the serializers may use the information to necessary
    /// to instantiate objects efficiently. 
    /// </summary>
    internal class L5XSerializers
    {
        private readonly Dictionary<Type, IL5XSerializer> _serializers;

        /// <summary>
        /// Creates a new <see cref="L5XSerializers"/> instance with the provided context.
        /// </summary>
        /// <param name="context">The <see cref="L5XContext"/> to pass down to the serializer instances.</param>
        public L5XSerializers(L5XContext context)
        {
            _serializers = new Dictionary<Type, IL5XSerializer>
            {
                { typeof(IController), new ControllerSerializer() },
                { typeof(IComplexType), new DataTypeSerializer(context) },
                { typeof(IUserDefined), new DataTypeSerializer(context) },
                { typeof(IModule), new ModuleSerializer() },
                { typeof(IAddOnInstruction), new AddOnInstructionSerializer() },
                { typeof(ITag<IDataType>), new TagSerializer() },
                { typeof(IProgram), new ProgramSerializer() },
                { typeof(ITask), new TaskSerializer() }
            };
        }
        
        
        /// <summary>
        /// Gets a serializer based on the specified <see cref="ILogixComponent"/> type.
        /// </summary>
        /// <typeparam name="TComponent">The logix component for which to retrieve a serializer.</typeparam>
        /// <returns>The serializer instance that maps to the specified component type.</returns>
        public IL5XSerializer<TComponent> GetFor<TComponent>() where TComponent : ILogixComponent
        {
            var target = _serializers.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;
            
            if (target is not IL5XSerializer<TComponent> serializer)
                throw new InvalidOperationException($"No serializer defined for'{typeof(TComponent)}'");

            return serializer;
        }
    }
}