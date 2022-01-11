using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization.Core
{
    /// <summary>
    /// Provides serialization of a <see cref="ILadderLogic"/> as represented in the L5X format. 
    /// </summary>
    public class LadderLogicSerializer : IXSerializer<ILadderLogic>
    {
        private static readonly XName ElementName = LogixNames.RllContent;
        
        /// <inheritdoc />
        public XElement Serialize(ILadderLogic component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            var rungs = new XElement(LogixNames.Rungs);
            rungs.Add(component.Select(r => r.Serialize()));
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

            var rungs = element.Descendants(LogixNames.Rung).Select(e => e.Deserialize<Rung>());

            return new LadderLogic(rungs);
        }
    }
}