using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Types;

namespace L5Sharp.Utilities
{
    internal class LogixTypeProvider
    {
        private readonly XElement _content;

        public LogixTypeProvider(XElement content)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public IDataType GetDataType(string name)
        {
            if (Logix.DataType.Contains(name))
                return Logix.DataType.Instantiate(name);

            var element = FindTypeDefinition(name);

            return element != null ? GetTypeSerializer(element).Deserialize(element) : new Undefined(name);
        }

        private IXSerializer<IDataType> GetTypeSerializer(XElement element)
        {
            if (element.Name == LogixNames.DataType)
                return (IXSerializer<IDataType>) new UserDefinedSerializer();

            if (element.Name == LogixNames.Structure || element.Name == LogixNames.StructureMember)
                return (IXSerializer<IDataType>) new StructureSerializer();

            if (element.Name == LogixNames.AddOnInstructionDefinition)
                throw new NotImplementedException();
            
            throw new NotSupportedException();
        }

        private XElement FindTypeDefinition(string name)
        {
            var userDefined = _content.Descendants(LogixNames.DataType).FirstOrDefault(x => x.GetName() == name);
            if (userDefined != null)
                return userDefined;

            var moduleDefined = _content.Descendants(LogixNames.Module)
                .Descendants().Where(x => x.Attribute(LogixNames.DataType) != null)
                .FirstOrDefault(x => x.GetDataTypeName() == name);
            
            if (moduleDefined != null)
                return moduleDefined;

            var addOnDefined = _content.Descendants(LogixNames.AddOnInstructionDefinition)
                .FirstOrDefault(x => x.GetName() == name);

            return addOnDefined;
        }
    }
}