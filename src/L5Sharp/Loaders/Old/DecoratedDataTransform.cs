using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public class DecoratedDataTransform : IMultiElementTransform
    {
        private readonly Dictionary<string, IElementTransform> _transforms = new Dictionary<string, IElementTransform>
        {
            { "Array", new ArrayElementTransform() },
            { "Structure", new StructureElementTransform() }
        };

        public IEnumerable<XElement> TransformMany(IEnumerable<XElement> elements)
        {
            return elements.SelectMany(TransformMany);
        }

        public IEnumerable<XElement> TransformMany(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            
            var members = new List<XElement>();
            
            if (!element.Name.ToString().Contains(L5XNames.Data))
                throw new InvalidOperationException($"XElement name is not expected value {L5XNames.Data}");

            var root = element.Elements().FirstOrDefault();
            if (root == null) return members;

            if (!_transforms.ContainsKey(root.Name.ToString()))
                throw new InvalidOperationException();

            var transform = _transforms[root.Name.ToString()];

            members.AddRange(root.Elements().Select(child => transform.Normalize(child)));
            return members;
        }
    }
}