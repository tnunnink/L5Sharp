using System.Xml.Linq;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Loaders
{
    public abstract class TagMemberTransform : IElementTransform
    {
        public virtual XElement Normalize(XElement element)
        {
            return null;
        }

        public XElement Revert(XElement element)
        {
            return null;
        }

        protected static XElement GenerateMember(string name, string dataType = null, string dimension = null,
            string radix = null, string access = null, string description = null, string value = null)
        {
            var element = new XElement(nameof(TagMember));

            element.Add(new XAttribute(L5XNames.Name, name));

            if (dataType != null)
                element.Add(new XAttribute(L5XNames.DataType, dataType));

            if (dimension != null)
                element.Add(new XAttribute(L5XNames.Dimension, dimension));

            if (radix != null)
                element.Add(new XAttribute(L5XNames.Radix, radix));

            if (access != null)
                element.Add(new XAttribute(L5XNames.ExternalAccess, access));
            
            if (description != null)
                element.Add(new XAttribute(L5XNames.Description, description));

            if (value != null)
                element.Add(new XAttribute(L5XNames.Value, value));

            return element;
        }
    }
}