using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Transforms
{
    public class ArrayMemberTransform : BaseTransform
    {
        public override XElement Perform(XElement element)
        {
            if (!element.Name.ToString().Equals(ElementNames.ArrayMember))
                throw new InvalidOperationException($"XElement name is not expected value {ElementNames.ArrayMember}");

            var name = element.Attribute(ElementNames.Name)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Attribute(nameof(ElementNames.DataType))?.Value;
            var dimension = element.Attribute(nameof(ElementNames.Dimension))?.Value;
            var radix = element.Attribute(nameof(ElementNames.Radix))?.Value;
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(ElementNames.ExternalAccess))?.Value;
            var description = element.Element(nameof(ElementNames.Description))?.Value;

            var transformed = Generate(name, dataType, dimension, radix, externalAccess, description);

            if (!element.HasElements) return transformed;

            foreach (var index in element.Elements())
            {
                var transform = new ArrayElementTransform();
                var member = transform.Perform(index);
                transformed.Add(member);
            }
            
            return transformed;
        }
    }
}