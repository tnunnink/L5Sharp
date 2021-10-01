using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class ArrayElementTransform : TagMemberTransform
    {
        public override XElement Normalize(XElement element)
        {
            if (!element.Name.ToString().Contains(L5XNames.Element))
                throw new InvalidOperationException($"XElement name is not expected value {L5XNames.Array}");

            var name = element.Attribute(L5XNames.Index)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Ancestors(L5XNames.Array).FirstOrDefault()?.Attribute(L5XNames.DataType)?.Value;
            var dimension = element.Ancestors(L5XNames.Array).FirstOrDefault()?.Attribute(L5XNames.Dimension)?.Value;
            var radix = element.Ancestors(L5XNames.Array).FirstOrDefault()?.Attribute(L5XNames.Radix)?.Value;
            var externalAccess = element.Ancestors(nameof(Tag)).FirstOrDefault()?.Attribute(L5XNames.ExternalAccess)?.Value;
            var description = element.Element(nameof(L5XNames.Description))?.Value;
            var value = element.Attribute(nameof(L5XNames.Value))?.Value;

            var transformed = GenerateMember(name, dataType, dimension, radix, externalAccess, description, value);

            var children = element.Element(L5XNames.Structure)?.Elements().ToList();

            if (children == null) return transformed;

            foreach (var member in from child in children
                let transform = new StructureElementTransform()
                select transform.Normalize(child))
            {
                transformed.Add(member);
            }

            return transformed;
        }
    }
}