using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp.Common
{
    /// <summary>
    /// Helper class for going out and finding and creating an instance of the current element's data type.
    /// </summary>
    internal class LogixTypeProvider
    {
        private readonly string _typeName;
        private readonly XDocument? _document;

        public LogixTypeProvider(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));
            
            if (element.Attribute(LogixNames.DataType) is null)
                throw new ArgumentException("Provided element does not have a data type attribute");
            
            _document = element.Document;
            _typeName = element.Attribute(LogixNames.DataType)?.Value!;
        }

        public IDataType FindType()
        {
            //First we can simply ask if this is a known type.
            if (Logix.DataType.Contains(_typeName))
                return Logix.DataType.Instantiate(_typeName);

            //Otherwise we need to find the definition from the element's XDocument (if not null).
            var definition = FindTypeDefinition(_typeName);

            //Deserialize type definition if found. Otherwise return Undefined.
            return definition is not null 
                ? GetTypeSerializer(definition).Deserialize(definition) 
                : new Undefined(_typeName);
        }

        private IXSerializer<IDataType> GetTypeSerializer(XElement element)
        {
            if (element.Name == LogixNames.DataType)
                return (IXSerializer<IDataType>) new DataTypeSerializer();

            if (element.Name == LogixNames.Structure || element.Name == LogixNames.StructureMember)
                return (IXSerializer<IDataType>) new StructureSerializer();

            if (element.Name == LogixNames.AddOnInstructionDefinition)
                throw new NotImplementedException();
            
            throw new NotSupportedException();
        }

        private XElement? FindTypeDefinition(string name)
        {
            //If we could not get a document, then there is nothing to search.
            if (_document is null) return null;
            
            //Is this a User Defined type that exists in the L5X?
            var userDefined = _document.Descendants(LogixNames.DataType).FirstOrDefault(x => x.GetComponentName() == name);
            if (userDefined != null)
                return userDefined;

            //Is this a Module Defined type that exists in the L5X?
            var moduleDefined = _document.Descendants(LogixNames.Module)
                .Descendants().Where(x => x.Attribute(LogixNames.DataType) != null)
                .FirstOrDefault(x => x.Attribute(LogixNames.DataType)?.Value == name);
            
            if (moduleDefined != null)
                return moduleDefined;

            //Is this a AOI Defined type that exists in the L5X?
            var addOnDefined = _document.Descendants(LogixNames.AddOnInstructionDefinition)
                .FirstOrDefault(x => x.GetComponentName() == name);

            return addOnDefined ?? null;
        }
    }
}