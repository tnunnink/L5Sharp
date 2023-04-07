using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Rll"/> components.
    /// </summary>
    public class RllSerializer : ILogixSerializer<Rll>
    {
        private readonly RungSerializer _rungSerializer = new();
        
        /// <inheritdoc />
        public XElement Serialize(Rll obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.RLLContent);
            element.Add(obj.Select(r => _rungSerializer.Serialize(r)));
            return element;
        }

        /// <inheritdoc />
        public Rll Deserialize(XElement element)
        {
            Check.NotNull(element);
            
            var rungs = element.Descendants(L5XName.Rung).Select(e => _rungSerializer.Deserialize(e));
            return new Rll(rungs);
        }
    }
}