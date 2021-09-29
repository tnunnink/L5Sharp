using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Transforms
{
    public class StructureElementTransform : BaseTransform
    {
        private readonly Dictionary<string, IElementTransform> _transforms = new Dictionary<string, IElementTransform>
        {
            { "DataValueMember", new DataValueMemberTransform() },
            { "ArrayMember", new ArrayMemberTransform() },
            { "StructureMember", new StructureMemberTransform() }
        };

        public override XElement Perform(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            
            if (!_transforms.ContainsKey(element.Name.ToString()))
                throw new InvalidOperationException(
                    $"The element {element.Name} does not have a specified transform instance");

            var transform = _transforms[element.Name.ToString()];

            return transform.Perform(element);
        }
    }
}