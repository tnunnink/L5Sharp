using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Components;
using L5Sharp.Serialization.Data;
using L5Sharp.Types;

namespace L5Sharp.Helpers
{
    /// <summary>
    /// A helper for finding and deserializing data types across an L5X document.
    /// </summary>
    internal class L5XTypeIndex
    {
        private readonly Dictionary<string, XElement> _index;

        internal L5XTypeIndex(XDocument document)
        {
            if (document is null)
                throw new ArgumentNullException(nameof(document));
            
            _index = new Dictionary<string, XElement>(StringComparer.OrdinalIgnoreCase);

            var l5X = document.Root ?? throw new ArgumentException("The provided XDocument must have a content root.");

            RegisterUserDefinedTypes(l5X);
            RegisterModuleDefinedTypes(l5X);
            RegisterAddOnDefinedTypes(l5X);
        }

        /// <summary>
        /// Gets an instance of the specified 
        /// </summary>
        /// <param name="name">The name of the data type to get.</param>
        /// <returns>A new <see cref="IDataType"/> instance that was deserialized from the L5X document if found.
        /// If not found, a new <see cref="Undefined"/> instance wrapping the provided name.</returns>
        public IDataType GetDataType(string name)
        {
            return _index.TryGetValue(name, out var type) ? DeserializeType(type) : new Undefined(name);
        }

        private static IDataType DeserializeType(XElement element)
        {
            if (element.Name == L5XElement.DataType.ToXName())
            {
                var serialize = new UserDefinedSerializer();
                return serialize.Deserialize(element);
            }

            if (element.Name == L5XElement.Structure.ToXName())
            {
                var serialize = new StructureSerializer();
                return serialize.Deserialize(element);
            }

            if (element.Name == L5XElement.AddOnInstructionDefinition.ToXName())
            {
                throw new NotImplementedException();
            }
            
            throw new InvalidOperationException(
                $"The element name {element.Name} is not supported by {typeof(L5XTypeIndex)}.");
        }

        private void RegisterUserDefinedTypes(XContainer document)
        {
            var types = document.Descendants(L5XElement.DataType.ToXName());

            foreach (var type in types)
            {
                var name = type.GetComponentName();
                _index.TryAdd(name, type);
            }
        }

        private void RegisterModuleDefinedTypes(XContainer document)
        {
            var types = document.Descendants(L5XElement.Module.ToXName()).Descendants(L5XElement.Structure.ToXName());

            foreach (var type in types)
            {
                var name = type.Attribute(L5XElement.DataType.ToXName())?.Value!;
                _index.TryAdd(name, type);
            }
        }

        private void RegisterAddOnDefinedTypes(XContainer document)
        {
            var types = document.Descendants(L5XElement.AddOnInstructionDefinition.ToXName());

            foreach (var type in types)
            {
                var name = type.GetComponentName();
                _index.TryAdd(name, type);
            }
        }
    }
}