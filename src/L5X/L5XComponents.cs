using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.L5X
{
    internal class L5XComponents
    {
        private readonly L5XDocument _document;

        public L5XComponents(L5XDocument document)
        {
            _document = document;
        }
        
        /// <summary>
        /// Gets all <see cref="XElement"/> objects that represent the component element for specified component type.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> to get the component elements for.</typeparam>
        /// <returns>A new collection of <see cref="XElement"/> instances that represent the component element for the
        /// specified component type.</returns>
        public IEnumerable<XElement> Get<TComponent>()
            where TComponent : ILogixComponent =>
            _document.Content.Descendants(L5XNames.GetComponentName<TComponent>())
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);
    }
}