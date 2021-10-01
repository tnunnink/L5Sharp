using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class MemberLoader : IElementLoader
    {
        private readonly IController _controller;

        public MemberLoader(IController controller)
        {
            _controller = controller;
        }

        public void Load(XElement element)
        {
            var parent = GetParentType(element);
            var dataType = GetMemberType(element);

            var member = new Member(element.GetName(), dataType, element.GetDimension(), element.GetRadix(),
                element.GetExternalAccess(), element.GetDescription(), element.GetHidden(), element.GetTarget(),
                element.GetBitNumber());
            
            parent.AddMember(member);
        }

        public void Load(IEnumerable<XElement> elements)
        {
            foreach (var element in elements)
                Load(element);
        }

        private DataType GetParentType(XNode element)
        {
            var typeName = element.Ancestors(L5XNames.DataType).FirstOrDefault()?.Attribute(L5XNames.Name)?.Value;
            if (typeName == null)
                throw new InvalidOperationException();

            var type = _controller.DataTypes.SingleOrDefault(d => d.Name == typeName);
            if (!(type is DataType dataType))
                throw new InvalidOperationException();

            return dataType;
        }

        private IDataType GetMemberType(XElement element)
        {
            var typeName = element.GetDataTypeName();
            
            if (Predefined.ContainsType(typeName))
                return Predefined.FromName(typeName);
            
            var type = _controller.DataTypes.SingleOrDefault(d => d.Name == typeName);
            if (type == null)
                throw new DataTypeNotFoundException();

            return type;
        }
    }
}