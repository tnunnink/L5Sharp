using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Transforms
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
            
            if (!element.Name.ToString().Contains(ElementNames.Data))
                throw new InvalidOperationException($"XElement name is not expected value {ElementNames.Data}");

            var first = element.Elements().FirstOrDefault();
            if (first == null) return members;

            if (!_transforms.ContainsKey(first.Name.ToString()))
                throw new InvalidOperationException();

            var transform = _transforms[first.Name.ToString()];

            members.AddRange(element.Elements().Select(child => transform.Perform(child)));
            return members;
        }
    }
}