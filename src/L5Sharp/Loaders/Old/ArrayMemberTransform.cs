using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class ArrayMemberTransform : TagMemberTransform
    {
        public override XElement Normalize(XElement element)
        {
            if (!element.Name.ToString().Equals(L5XNames.ArrayMember))
                throw new InvalidOperationException($"XElement name is not expected value {L5XNames.ArrayMember}");

            var name = element.Attribute(L5XNames.Name)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Attribute(nameof(L5XNames.DataType))?.Value;
            var dimension = element.Attribute(nameof(L5XNames.Dimension))?.Value;
            var radix = element.Attribute(nameof(L5XNames.Radix))?.Value;
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(L5XNames.ExternalAccess))?.Value;
            var description = element.Element(nameof(L5XNames.Description))?.Value;

            var transformed = GenerateMember(name, dataType, dimension, radix, externalAccess, description);

            if (!element.HasElements) return transformed;

            foreach (var index in element.Elements())
            {
                var transform = new ArrayElementTransform();
                var member = transform.Normalize(index);
                transformed.Add(member);
            }
            
            return transformed;
        }
    }
}