using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Predefined;
using L5Sharp.Serialization.Components;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Indexing
{
    internal class DataTypeIndex : IL5XIndex<IDataType>
    {
        private readonly Dictionary<string, XElement> _index;
        private readonly StructureSerializer _structureSerializer;
        private readonly DataTypeSerializer _dataTypeSerializer;
        private readonly AddOnInstructionSerializer _addOnInstructionSerializer;

        internal DataTypeIndex(L5XContext context)
        {
            _index = new Dictionary<string, XElement>(StringComparer.OrdinalIgnoreCase);

            RegisterUserDefinedTypes(context.L5X.Content);
            RegisterModuleDefinedTypes(context.L5X.Content);
            RegisterAddOnDefinedTypes(context.L5X.Content);

            _dataTypeSerializer = new DataTypeSerializer(context);
            _structureSerializer = new StructureSerializer();
            _addOnInstructionSerializer = new AddOnInstructionSerializer(context);
        }

        public IDataType Lookup(string name)
        {
            if (DataType.Exists(name))
                return DataType.Create(name);

            return _index.TryGetValue(name, out var element) ? DeserializeType(element) : new Undefined(name);
        }

        private IDataType DeserializeType(XElement element)
        {
            return element.Name == L5XElement.DataType.ToString() ? _dataTypeSerializer.Deserialize(element)
                : element.Name == L5XElement.Structure.ToString() ? _structureSerializer.Deserialize(element)
                : _addOnInstructionSerializer.Deserialize(element);
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