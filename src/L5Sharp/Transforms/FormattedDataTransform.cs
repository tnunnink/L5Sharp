using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Transforms
{
    public class FormattedDataTransform : IMultiElementTransform
    {
        private readonly Dictionary<string, IMultiElementTransform> _transforms = new Dictionary<string, IMultiElementTransform>
        {
            { "Decorated", new DecoratedDataTransform() },
            //{ "String", new ArrayMemberTransform() },
            //{ "Alarm", new StructureMemberTransform() }
        };
        
        public IEnumerable<XElement> TransformMany(IEnumerable<XElement> elements)
        {
            return elements.SelectMany(TransformMany);
        }

        public IEnumerable<XElement> TransformMany(XElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            
            var format = element.FirstAttribute.Value;

            if (!_transforms.ContainsKey(format))
                throw new InvalidOperationException(
                    $"The element {element.Name} does not have a specified transform instance");

            var transform = _transforms[format];
            return transform.TransformMany(element.Elements().FirstOrDefault());
        }
    }
}