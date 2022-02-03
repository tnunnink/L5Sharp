using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp
{
    /// <summary>
    /// A class that indexes all data types of a L5X so that we can easily deserialize them when they are requested.
    /// </summary>
    internal class LogixTypeIndex
    {
        private readonly LogixContext _context;
        private readonly Dictionary<string, XElement> _index;

        internal LogixTypeIndex(LogixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _index = new Dictionary<string, XElement>(StringComparer.OrdinalIgnoreCase);

            RegisterUserDefinedTypes(_context.L5X.Root);
            RegisterModuleDefinedTypes(_context.L5X.Root);
            RegisterAddOnDefinedTypes(_context.L5X.Root);
        }

        /// <summary>
        /// Gets an instance of the specified 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDataType GetDataType(string name)
        {
            if (DataType.Exists(name))
                return DataType.Create(name);

            return _index.TryGetValue(name, out var type) ? DeserializeType(type) : new Undefined(name);
        }

        private IDataType DeserializeType(XElement element)
        {
            if (element.Name == LogixNames.DataType)
                return DeserializeUserDefined(element);

            if (element.Name == LogixNames.Structure)
                return DeserializeStructure(element);

            if (element.Name == LogixNames.AddOnInstructionDefinition)
                return DeserializeAddOnDefined(element);

            throw new InvalidOperationException(
                $"The provided element name {element.Name} is not supported for data type serialization.");
        }

        private IDataType DeserializeUserDefined(XElement element)
        {
            var serialize = new UserDefinedSerializer(_context);
            return serialize.Deserialize(element);
        }

        private IDataType DeserializeStructure(XElement element)
        {
            var serialize = new StructureSerializer();
            return serialize.Deserialize(element);
        }

        private IDataType DeserializeAddOnDefined(XElement element)
        {
            throw new NotImplementedException();
        }

        private void RegisterUserDefinedTypes(XContainer document)
        {
            var types = document.Descendants(LogixNames.DataType);

            foreach (var type in types)
            {
                var name = type.GetComponentName();
                _index.TryAdd(name, type);
            }
        }

        private void RegisterModuleDefinedTypes(XContainer document)
        {
            var types = document.Descendants(LogixNames.Module).Descendants(LogixNames.Structure);

            foreach (var type in types)
            {
                var name = type.Attribute(LogixNames.DataType)?.Value!;
                _index.TryAdd(name, type);
            }
        }

        private void RegisterAddOnDefinedTypes(XContainer document)
        {
            var types = document.Descendants(LogixNames.AddOnInstructionDefinition);

            foreach (var type in types)
            {
                var name = type.GetComponentName();
                _index.TryAdd(name, type);
            }
        }
    }
}