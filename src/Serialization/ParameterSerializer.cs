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
        private static readonly XName ElementName = L5XName.Parameter;
        
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
            element.Add(new XAttribute(L5XName.TagType, component.TagType));
            
            if (component.TagType == TagType.Base)
                element.Add(new XAttribute(L5XName.DataType, component.DataType.Name));
            
            if (!component.Dimensions.IsEmpty)
                element.Add(new XAttribute(L5XName.Dimensions, component.Dimensions));
            
            element.Add(new XAttribute(L5XName.Usage, component.Usage));
            
            if (component.Radix != Radix.Null)
                element.Add(new XAttribute(L5XName.Radix, component.Radix));
            
            if (component.TagType == TagType.Alias && component.Alias is not null)
                element.Add(new XAttribute(L5XName.AliasFor, component.Alias.Name));
            
            element.Add(new XAttribute(L5XName.Required, component.Required));
            element.Add(new XAttribute(L5XName.Visible, component.Visible));
            
            if (component.Usage == TagUsage.InOut)
                element.Add(new XAttribute(L5XName.Constant, component.Constant));
            
            if (component.Usage != TagUsage.InOut)
                element.Add(new XAttribute(L5XName.ExternalAccess, component.ExternalAccess));

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
            var dimensions = element.Attribute(L5XName.Dimensions)?.Value.Parse<Dimensions>();
            var radix = element.Attribute(L5XName.Radix)?.Value.Parse<Radix>();
            var access = element.Attribute(L5XName.ExternalAccess)?.Value.Parse<ExternalAccess>();
            var usage = element.Attribute(L5XName.Usage)?.Value.Parse<TagUsage>();
            var required = element.Attribute(L5XName.Required)?.Value.Parse<bool>() ?? default;
            var visible = element.Attribute(L5XName.Visible)?.Value.Parse<bool>() ?? default;
            var constant = element.Attribute(L5XName.Constant)?.Value.Parse<bool>() ?? default;

            var type = element.Attribute(L5XName.TagType)?.Value.Parse<TagType>();

            if (type == TagType.Alias)
            {
                var aliasFor = element.Attribute(L5XName.AliasFor)?.Value.Parse<TagName>();
                var localTag = element.Ancestors(L5XName.AddOnInstructionDefinition)
                    .Descendants(L5XName.LocalTag)
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