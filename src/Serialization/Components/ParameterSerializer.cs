using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization.Components
{
    internal class ParameterSerializer : IL5XSerializer<IParameter<IDataType>>
    {
        private readonly L5XContext? _context;
        private static readonly XName ElementName = L5XElement.Parameter.ToString();

        public ParameterSerializer()
        {
        }
        
        public ParameterSerializer(L5XContext context)
        {
            _context = context;
        }
        
        public XElement Serialize(IParameter<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.TagType.ToString(), component.TagType));
            element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            if (!component.Dimensions.IsEmpty)
                element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            element.Add(new XAttribute(L5XAttribute.Usage.ToString(), component.Usage));
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            if (!component.Alias.IsEmpty)
                element.Add(new XAttribute(L5XAttribute.AliasFor.ToString(), component.Alias));
            element.Add(new XAttribute(L5XAttribute.Required.ToString(), component.Required));
            element.Add(new XAttribute(L5XAttribute.Visible.ToString(), component.Visible));
            if (component.Usage == TagUsage.InOut)
                element.Add(new XAttribute(L5XAttribute.Constant.ToString(), component.Constant));
            if (component.Usage != TagUsage.InOut)
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
            var dataType = _context is not null
                ? _context.Index.GetDataType(element.DataTypeName())
                : DataType.Create(element.DataTypeName());
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