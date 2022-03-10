using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Predefined;

namespace L5Sharp.L5X
{
    internal class L5XTypeIndex
    {
        private readonly L5XContext _context;
        private readonly Dictionary<string, XElement> _index;

        internal L5XTypeIndex(L5XContext context)
        {
            _context = context;
            _index = new Dictionary<string, XElement>(StringComparer.OrdinalIgnoreCase);

            RegisterUserDefinedTypes(context.L5X.Content);
            RegisterModuleDefinedTypes(context.L5X.Content);
            RegisterAddOnDefinedTypes(context.L5X.Content);
        }


        public IDataType GetDataType(string name)
        {
            if (DataType.Exists(name))
                return DataType.Create(name);

            return _index.TryGetValue(name, out var element) ? DeserializeType(element) : new Undefined(name);
        }

        private IDataType DeserializeType(XElement element)
        {
            if (element.Name == L5XElement.DataType.ToString())
            {
                var serialize = _context.Serializers.GetSerializer<IUserDefined>();
                return serialize.Deserialize(element);
            }

            if (element.Name == L5XElement.Structure.ToString())
            {
                var serialize = _context.Serializers.GetSerializer<IComplexType>(element);
                return serialize.Deserialize(element);
            }

            //var serialize = _context.GetSerializer<IAddOnInstruction>(element);
            return new Undefined();
        }

        private void RegisterUserDefinedTypes(XContainer? container)
        {
            if (container is null) return;

            var types = container.Descendants(L5XElement.DataType.ToString());

            foreach (var type in types)
            {
                var name = type.ComponentName();
                _index.TryAdd(name, type);
            }
        }

        private void RegisterModuleDefinedTypes(XContainer? container)
        {
            if (container is null) return;

            var types = container
                .Descendants(L5XElement.Module.ToString())
                .Descendants(L5XElement.Structure.ToString());

            foreach (var type in types)
            {
                var name = type.Attribute(L5XElement.DataType.ToString())?.Value!;
                _index.TryAdd(name, type);
            }
        }

        private void RegisterAddOnDefinedTypes(XContainer? container)
        {
            if (container is null) return;

            var types = container.Descendants(L5XElement.AddOnInstructionDefinition.ToString());

            foreach (var type in types)
            {
                var name = type.ComponentName();
                _index.TryAdd(name, type);
            }
        }
    }
}