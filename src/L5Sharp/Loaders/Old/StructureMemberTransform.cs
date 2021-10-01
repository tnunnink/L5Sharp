using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class StructureMemberTransform : TagMemberTransform
    {
        public override XElement Normalize(XElement element)
        {
            if (!element.Name.ToString().Equals(L5XNames.StructureMember))
                throw new InvalidOperationException($"XElement name is not expected value {L5XNames.StructureMember}");

            var name = element.Attribute(L5XNames.Name)?.Value ?? throw new ArgumentNullException();
            var dataType = element.Attribute(nameof(L5XNames.DataType))?.Value;
            var externalAccess = element.Ancestors(nameof(Tag))
                .FirstOrDefault()?.Attribute(nameof(L5XNames.ExternalAccess))?.Value;
            
            var transformed = GenerateMember(name, dataType, access: externalAccess);

            var children = element.Elements();
            
            foreach (var child in children)
            {
                var transform = new StructureElementTransform();
                var members = transform.Normalize(child);
                transformed.Add(members);
            }
            
            return transformed;
        }
    }
}