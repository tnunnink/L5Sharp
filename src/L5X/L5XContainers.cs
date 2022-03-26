using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.L5X
{
    internal class L5XContainers
    {
        private readonly L5XDocument _document;

        public L5XContainers(L5XDocument document)
        {
            _document = document;
        }
        
        /// <summary>
        /// Gets an <see cref="XElement"/> that represents the containing element for the specified component type.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> to get the containing element for.</typeparam>
        /// <returns>A new <see cref="XElement"/> instance that represents the containing element for the specified
        /// component type.</returns>
        /// <remarks>
        /// For instance, if the specified component type is an <see cref="IUserDefined"/> component, then the method
        /// will return the element <see cref="L5XElement.DataTypes"/>, which corresponds to the containing element
        /// for all user defined data type.
        /// </remarks>
        public XElement Get<TComponent>() where TComponent : ILogixComponent => 
            _document.Content.Descendants(L5XNames.GetContainerName<TComponent>()).First();

        public void Create<TComponent>() where TComponent : ILogixComponent =>
            _document.Controller?.Add(new XElement(L5XNames.GetContainerName<TComponent>()));
    }
}