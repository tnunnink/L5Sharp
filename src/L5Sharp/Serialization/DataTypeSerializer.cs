using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IComponentSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            var element = new XElement(LogixNames.Components.DataType);
            element.Add(component.ToXAttribute(c => c.Name));
            element.Add(component.ToXAttribute(c => c.Family));
            element.Add(component.ToXAttribute(c => c.Class));

            if (!string.IsNullOrEmpty(component.Description))
                element.Add(component.ToXCDataElement(c => c.Description));

            element.Add(new XElement(nameof(component.Members),
                component.Members.Select(m => m.Serialize())));

            return element;
        }
    }
}