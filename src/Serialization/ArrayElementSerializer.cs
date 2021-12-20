using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Components;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    internal class ArrayElementSerializer : IXSerializer<IMember<IDataType>>
    {
        private const string ElementName = LogixNames.Element;

        public XElement Serialize(IMember<IDataType> component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(LogixNames.Element);

            element.Add(component.ToAttribute(x => x.Name, LogixNames.Index));

            switch (component.DataType)
            {
                case IAtomicType atomic:
                    element.Add(atomic.ToAttribute(x => x.Value));
                    break;
                case IComplexType complexType:
                {
                    var structureSerializer = new StructureSerializer();
                    var structure = structureSerializer.Serialize(complexType);
                    element.Add(structure);
                    break;
                }
            }

            return element;
        }

        public IMember<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            //todo this is not right we need index
            var name = element.GetName();

            var dataType = element.Element(LogixNames.Structure) is null
                           && element.Attribute("Value") is not null
                ? element.GetDataType()
                : new StructureSerializer().Deserialize(element.Element(LogixNames.Structure)!);

            if (dataType is not IAtomicType atomic) return Member.Create(name!, dataType);

            var value = element.GetValue<IAtomicType, object>(a => a.Value);
            atomic.SetValue(value!);

            return Member.Create(name, atomic);
        }
    }
}