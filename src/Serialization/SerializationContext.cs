using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal class SerializationContext
    {
        private readonly Dictionary<Type, IXSerializer> _serializers = new Dictionary<Type, IXSerializer>();

        public SerializationContext(LogixContext context)
        {
            _serializers.Add(typeof(IUserDefined), new UserDefinedSerializer(context));
            _serializers.Add(typeof(IMember<IDataType>), new MemberSerializer(context));
            _serializers.Add(typeof(ITag<IDataType>), new TagSerializer(context));
            _serializers.Add(typeof(ITask), new TaskSerializer());
            
        }
        
        public XElement Serialize<T>(T component)
        {
            var serializer = GetSerializer<T>();
            return serializer.Serialize(component);
        }

        public T Deserialize<T>(XElement element)
        {
            var serializer = GetSerializer<T>();
            return serializer.Deserialize(element);
        }

        private IXSerializer<T> GetSerializer<T>()
        {
            var type = typeof(T);

            if (!_serializers.ContainsKey(type))
                throw new InvalidOperationException($"Serializer not defined for component of type '{type}'");

            return (IXSerializer<T>) _serializers[type];
        }
    }
}