using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Transforms
{
    public class StructureMemberTransform : BaseTransform
    {
        public override XElement Perform(XElement element)
        {
            if (!element.Name.ToString().Equals(ElementNames.StructureMember))
                throw new InvalidOperationException($"XElement name is not expected value {ElementNames.StructureMember}");

            var name = element.Attribute(ElementNames.Name)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Attribute(nameof(ElementNames.DataType))?.Value;
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(ElementNames.ExternalAccess))?.Value;
            
            var transformed = Generate(name, dataType, access: externalAccess);

            var children = element.Elements();
            
            foreach (var child in children)
            {
                var transform = new StructureElementTransform();
                var members = transform.Perform(child);
                transformed.Add(members);
            }
            
            return transformed;
        }
    }
}