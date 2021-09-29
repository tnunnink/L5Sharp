using System.Xml.Linq;
using L5Sharp.Primitives;

namespace L5Sharp.Transforms
{
    public abstract class BaseTransform : IElementTransform
    {
        public virtual XElement Perform(XElement element)
        {
            return null;
        }

        protected static XElement Generate(string name, string dataType = null, string dimension = null,
            string radix = null, string access = null, string description = null, string value = null)
        {
            var element = new XElement(nameof(TagMember));

            element.Add(new XAttribute(ElementNames.Name, name));

            if (dataType != null)
                element.Add(new XAttribute(ElementNames.DataType, dataType));

            if (dimension != null)
                element.Add(new XAttribute(ElementNames.Dimension, dimension));

            if (radix != null)
                element.Add(new XAttribute(ElementNames.Radix, radix));

            if (access != null)
                element.Add(new XAttribute(ElementNames.ExternalAccess, access));
            
            if (description != null)
                element.Add(new XAttribute(ElementNames.Description, description));

            if (value != null)
                element.Add(new XAttribute(ElementNames.Value, value));

            return element;
        }
    }
}