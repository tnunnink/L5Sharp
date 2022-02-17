using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// Provides serialization of a <see cref="ILadderLogic"/> as represented in the L5X format. 
    /// </summary>
    internal class LadderLogicSerializer : IXSerializer<ILadderLogic>
    {
        private readonly LogixContext _context;
        private static readonly XName ElementName = LogixNames.RllContent;

        public LadderLogicSerializer(LogixContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public XElement Serialize(ILadderLogic component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var rungs = new XElement(LogixNames.Rungs);
            rungs.Add(component.Select(r => _context.Serializer.Serialize(r)));
            element.Add(rungs);

            return element;
        }

        /// <inheritdoc />
        public ILadderLogic Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element name '{element.Name}' invalid. Expecting '{ElementName}'");

            var rungs = element.Descendants(LogixNames.Rung).Select(e => _context.Serializer.Deserialize<Rung>(e));

            return new LadderLogic(rungs);
        }
    }
}