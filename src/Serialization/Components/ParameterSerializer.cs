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
        private readonly Func<string, IDataType> _dataTypeCreator;

        public ParameterSerializer()
        {
            _dataTypeCreator = DataType.Create;
        }
        
        public ParameterSerializer(L5XContext context)
        {
            _dataTypeCreator = context.Index.GetDataType;
        }
        
        public XElement Serialize(IParameter<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.Add(new XAttribute(L5XAttribute.Name.ToString(), component.Name));
            element.Add(new XElement(L5XElement.Description.ToString(), new XCData(component.Description)));
            element.Add(new XAttribute(L5XAttribute.TagType.ToString(), component.TagType));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            element.Add(new XAttribute(L5XAttribute.Usage.ToString(), component.Usage));
            element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            element.Add(new XAttribute(L5XAttribute.Required.ToString(), component.Required));
            element.Add(new XAttribute(L5XAttribute.Visible.ToString(), component.Visible));
            element.Add(new XAttribute(L5XAttribute.Constant.ToString(), component.Constant));
            element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));

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
            var dataType = _dataTypeCreator.Invoke(element.DataTypeName());
            var dimensions = element.Attribute(L5XAttribute.Dimensions.ToString())?.Value.Parse<Dimensions>();    
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value.Parse<Radix>();    
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value.Parse<ExternalAccess>();    
            var usage = element.Attribute(L5XAttribute.Usage.ToString())?.Value.Parse<TagUsage>();    
            var alias = element.Attribute(L5XAttribute.AliasFor.ToString())?.Value.Parse<TagName>();    
            var required = element.Attribute(L5XAttribute.Required.ToString())?.Value.Parse<bool>() ?? default;    
            var visible = element.Attribute(L5XAttribute.Visible.ToString())?.Value.Parse<bool>() ?? default;    
            var constant = element.Attribute(L5XAttribute.Constant.ToString())?.Value.Parse<bool>() ?? default;    
            
            if (dimensions is null || dimensions.IsEmpty)
                return new Parameter<IDataType>(name, dataType, radix, access, usage, alias, required, visible,
                    constant, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Parameter<IDataType>(name, arrayType, radix, access, usage, alias, required, visible,
                constant, description);
        }
    }
}