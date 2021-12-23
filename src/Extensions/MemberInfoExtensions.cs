using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace L5Sharp.Extensions
{
    internal static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the XML Attribute name for the property of the current member expression. 
        /// </summary>
        /// <returns>
        /// A string value representing the name of the member or the XmlAttribute attribute name if it sets
        /// on the member.
        /// </returns>
        public static XName GetXName(this MemberInfo member)
        {
           var xName = (string)member.CustomAttributes
                .FirstOrDefault(x => x.AttributeType == typeof(XmlAttributeAttribute)
                                     || x.AttributeType == typeof(XmlElementAttribute))
                ?.ConstructorArguments.First().Value;


            return xName ?? member.Name;
        }

        /// <summary>
        /// Gets the method info for either XElement.Element(XName) or XElement.Attribute(XName) depending on whether the
        /// member has a custom XmlElementAttribute declaration.
        /// </summary>
        /// <returns>
        /// A MethodInfo for XElement.Element() if the member expression is decorated with XmlElementAttribute.
        /// Otherwise, an MethodInfo object for XElement.Attribute().
        /// </returns>
        public static MethodInfo GetXMethod(this MemberInfo member)
        {
            var methodName = member.CustomAttributes.Any(x => x.AttributeType == typeof(XmlElementAttribute))
                ? "Element"
                : "Attribute";
            
            var signature = new[] {typeof (XName)};

            // We know the methods we are calling exist so don't need to worry about potential null reference.
            return typeof(XElement).GetMethod(methodName, signature)!;
        }
    }
}