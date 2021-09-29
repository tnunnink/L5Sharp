using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Transforms
{
    public class ArrayElementTransform : BaseTransform
    {
        public override XElement Perform(XElement element)
        {
            if (!element.Name.ToString().Contains(ElementNames.Array))
                throw new InvalidOperationException($"XElement name is not expected value {ElementNames.Array}");

            var name = element.Attribute(ElementNames.Index)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Ancestors(ElementNames.Array).FirstOrDefault()?.Attribute(ElementNames.DataType)?.Value;
            var dimension = element.Ancestors(ElementNames.Array).FirstOrDefault()?.Attribute(ElementNames.Dimension)?.Value;
            var radix = element.Ancestors(ElementNames.Array).FirstOrDefault()?.Attribute(ElementNames.Radix)?.Value;
            var externalAccess = element.Ancestors(nameof(Tag)).FirstOrDefault()?.Attribute(ElementNames.ExternalAccess)?.Value;
            var description = element.Element(nameof(ElementNames.Description))?.Value;
            var value = element.Attribute(nameof(ElementNames.Value))?.Value;

            var transformed = Generate(name, dataType, dimension, radix, externalAccess, description, value);

            var children = element.Element(ElementNames.Structure)?.Elements().ToList();

            if (children == null) return transformed;

            foreach (var member in from child in children
                let transform = new StructureElementTransform()
                select transform.Perform(child))
            {
                transformed.Add(member);
            }

            return transformed;
        }
    }
}