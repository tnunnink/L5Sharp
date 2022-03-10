using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Predefined;

namespace L5Sharp.Serialization.Components
{
    internal class ParameterSerializer : IL5XSerializer<IParameter<IDataType>>
    {
        private static readonly XName ElementName = L5XElement.Parameter.ToString();
        private static readonly string Dimension = L5XAttribute.Dimension.ToString();
        private readonly L5XTypeIndex? _index;

        public ParameterSerializer(L5XTypeIndex? index = null)
        {
            _index = index;
        }
        
        public XElement Serialize(IParameter<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(nameof(component.Name), component.Name));
            element.Add(new XElement(nameof(component.Description), new XCData(component.Description)));
            element.Add(new XAttribute(nameof(component.TagType), component.TagType));
            element.Add(new XAttribute(nameof(component.DataType), component.DataType.Name));
            element.Add(new XAttribute(Dimension, component.Dimensions));
            element.Add(new XAttribute(nameof(component.Usage), component.Usage));
            element.Add(new XAttribute(nameof(component.Radix), component.Radix));
            element.Add(new XAttribute(nameof(component.Required), component.Required));
            element.Add(new XAttribute(nameof(component.Visible), component.Visible));
            element.Add(new XAttribute(nameof(component.Constant), component.Constant));
            element.Add(new XAttribute(nameof(component.ExternalAccess), component.ExternalAccess));

            return element;
        }

        public IParameter<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var typeName = element.DataTypeName();
            var dataType = DataType.Exists(typeName) ? DataType.Create(typeName) :
                _index is not null ? _index.GetDataType(typeName) : new Undefined(typeName);
            var dimensions = element.GetAttribute<Parameter<IDataType>, Dimensions>(m => m.Dimensions, Dimension);
            var radix = element.GetAttribute<Parameter<IDataType>, Radix>(m => m.Radix);
            var access = element.GetAttribute<Parameter<IDataType>, ExternalAccess>(m => m.ExternalAccess);
            var usage = element.GetAttribute<Parameter<IDataType>, TagUsage>(m => m.Usage);
            var alias = element.GetAttribute<Parameter<IDataType>, TagName?>(m => m.Alias);
            var required = element.GetAttribute<Parameter<IDataType>, bool>(m => m.Required);
            var visible = element.GetAttribute<Parameter<IDataType>, bool>(m => m.Visible);
            var constant = element.GetAttribute<Parameter<IDataType>, bool>(m => m.Constant);

            if (dimensions is null || dimensions.AreEmpty)
                return new Parameter<IDataType>(name, dataType, radix, access, usage, alias, required, visible,
                    constant, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Parameter<IDataType>(name, arrayType, radix, access, usage, alias, required, visible,
                constant, description);
        }
    }
}