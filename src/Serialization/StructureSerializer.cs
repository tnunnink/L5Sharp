using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class StructureSerializer : IXSerializer<IComplexType>
    {
        private readonly LogixContext _context;
        private const string ElementName = LogixNames.Structure;

        public StructureSerializer(LogixContext context)
        {
            _context = context;
        }

        public XElement Serialize(IComplexType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(LogixNames.Data);
            element.Add(component.ToAttribute(x => x.Format));

            var structure = new XElement(ElementName);
            structure.Add(component.ToAttribute(c => c.Name));

            //todo add members
            
            element.Add(structure);
            
            return element;
        }

        public IComplexType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var name = element.GetDataTypeName();
            var members = new List<IMember<IDataType>>();

            foreach (var child in element.Elements())
            {
                if (child.Name == LogixNames.StructureMember)
                    members.Add(Member.Create(child.GetName(), Deserialize(child)));

                members.Add(_context.Serialization.Deserialize<IMember<IDataType>>(child));
            }
            
            return new DataType(name, DataTypeClass.Unknown, string.Empty, members);
        }
    }
}