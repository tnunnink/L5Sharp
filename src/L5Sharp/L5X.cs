using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp
{
    public class L5X
    {
        private readonly XDocument _doc;

        private readonly Dictionary<Type, IL5XSerializer> _serializers =
            new Dictionary<Type, IL5XSerializer>
            {
                { typeof(DataType), new DataTypeSerializer() }
            };

        private readonly Dictionary<Type, string> _containerName = new Dictionary<Type, string>
        {
            { typeof(DataType), L5XNames.Containers.DataTypes },
        };

        public L5X(string fileName)
        {
            _doc = XDocument.Load(fileName);
        }

        public L5X(IController controller)
        {
        }

        public T Get<T>(string name) where T : INamedComponent
        {
            var type = typeof(T);
            var elementName = type.Name;

            if (!_serializers.ContainsKey(type)) return default;

            var serializer = (IL5XSerializer<T>)_serializers[type];

            var element = _doc.Descendants(elementName)
                .SingleOrDefault(x => x.Attribute(L5XNames.Name)?.Value == name);

            return serializer.Deserialize(element);
        }

        public void Add<T>(T item) where T : INamedComponent
        {
            var type = typeof(T);
            var elementName = type.Name;

            if (_serializers.ContainsKey(type))
                throw new InvalidOperationException();

            var serializer = (IL5XSerializer<T>)_serializers[type];

            var element = serializer.Serialize(item);

            var container = GetComponent(elementName);

            container.Add(element);

            /*var element = _doc.Descendants(elementName)
                .SingleOrDefault(x => x.Attribute(L5XNames.Name)?.Value == name);*/
        }

        private XElement GetComponent(string name)
        {
            return _doc.Descendants(name).FirstOrDefault()?.Parent;
        }
    }
}