using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Serialization
{
    internal class ParameterSerializer : L5XSerializer<IParameter<IDataType>>
    {
        private readonly L5XContent? _document;
        private static readonly XName ElementName = L5XElement.Parameter.ToString();
        
        private LocalTagSerializer LocalTagSerializer => _document is not null
            ? _document.Serializers.Get<LocalTagSerializer>()
            : new LocalTagSerializer(_document);

        public ParameterSerializer(L5XContent? document = null)
        {
            _document = document;
        }

        public override XElement Serialize(IParameter<IDataType> component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);
            element.AddComponentName(component.Name);
            element.AddComponentDescription(component.Description);
            element.Add(new XAttribute(L5XAttribute.TagType.ToString(), component.TagType));
            
            if (component.TagType == TagType.Base)
                element.Add(new XAttribute(L5XAttribute.DataType.ToString(), component.DataType.Name));
            
            if (!component.Dimensions.IsEmpty)
                element.Add(new XAttribute(L5XAttribute.Dimensions.ToString(), component.Dimensions));
            
            element.Add(new XAttribute(L5XAttribute.Usage.ToString(), component.Usage));
            
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(L5XAttribute.Radix.ToString(), component.Radix));
            
            if (component.TagType == TagType.Alias && component.Alias is not null)
                element.Add(new XAttribute(L5XAttribute.AliasFor.ToString(), component.Alias.Name));
            
            element.Add(new XAttribute(L5XAttribute.Required.ToString(), component.Required));
            element.Add(new XAttribute(L5XAttribute.Visible.ToString(), component.Visible));
            
            if (component.Usage == TagUsage.InOut)
                element.Add(new XAttribute(L5XAttribute.Constant.ToString(), component.Constant));
            
            if (component.Usage != TagUsage.InOut)
                element.Add(new XAttribute(L5XAttribute.ExternalAccess.ToString(), component.ExternalAccess));

            return element;
        }

        public override IParameter<IDataType> Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");

            var name = element.ComponentName();
            var description = element.ComponentDescription();
            var dimensions = element.Attribute(L5XAttribute.Dimensions.ToString())?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XAttribute.Radix.ToString())?.Value.Parse<Radix>();
            var access = element.Attribute(L5XAttribute.ExternalAccess.ToString())?.Value.Parse<ExternalAccess>();
            var usage = element.Attribute(L5XAttribute.Usage.ToString())?.Value.Parse<TagUsage>();
            var required = element.Attribute(L5XAttribute.Required.ToString())?.Value.Parse<bool>() ?? default;
            var visible = element.Attribute(L5XAttribute.Visible.ToString())?.Value.Parse<bool>() ?? default;
            var constant = element.Attribute(L5XAttribute.Constant.ToString())?.Value.Parse<bool>() ?? default;

            var type = element.Attribute(L5XAttribute.TagType.ToString())?.Value.Parse<TagType>();

            if (type == TagType.Alias)
            {
                var aliasFor = element.Attribute(L5XAttribute.AliasFor.ToString())?.Value.Parse<TagName>();
                var localTag = element.Ancestors(L5XElement.AddOnInstructionDefinition.ToString())
                    .Descendants(L5XElement.LocalTag.ToString())
                    .First(e => e.ComponentName() == aliasFor);
                var alias = LocalTagSerializer.Deserialize(localTag);
                
                return new Parameter<IDataType>(name, alias, radix, access, usage, required, visible, constant,
                    description);
            }
            
            var dataType = _document is not null
                ? _document.Index.LookupType(element.DataTypeName())
                : DataType.Create(element.DataTypeName());
            
            if (dimensions is null || dimensions.IsEmpty)
                return new Parameter<IDataType>(name, dataType, radix, access, usage, required, visible,
                    constant, description);

            var arrayType = new ArrayType<IDataType>(dimensions, dataType, radix, access, description);
            return new Parameter<IDataType>(name, arrayType, radix, access, usage, required, visible,
                constant, description);
        }
    }
}