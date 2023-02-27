using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
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

            var element = new XElement(L5XName.Routine);

            element.AddValue(obj, r => r.Name);
            element.AddText(obj, r => r.Description);
            element.AddValue(obj, r => r.Type);

            var content = new XElement(L5XName.RLLContent);
            content.Add(obj.Content.Select(r => _rungSerializer.Serialize(r)));
            element.Add(content);

            return element;
        }

        /// <inheritdoc />
        public Rll Deserialize(XElement element)
        {
            Check.NotNull(element);

            return new Rll
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Content = element.Descendants(L5XName.Rung).Select(r => _rungSerializer.Deserialize(r)).ToList()
            };
        }
    }
}