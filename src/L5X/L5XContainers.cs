using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.L5X
{
    internal class L5XContainers
    {
        private static readonly Dictionary<Type, string> Containers = new()
        {
            { typeof(IComplexType), L5XElement.DataTypes.ToString() },
            { typeof(IModule), L5XElement.Modules.ToString() },
            { typeof(IAddOnInstruction), L5XElement.AddOnInstructionDefinitions.ToString() },
            { typeof(ITag<IDataType>), L5XElement.Tags.ToString() },
            { typeof(IProgram), L5XElement.Programs.ToString() },
            { typeof(ITask), L5XElement.Tasks.ToString() }
        };

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
        public XElement Get<TComponent>() where TComponent : ILogixComponent
        {
            var name = Containers.FirstOrDefault(t => typeof(TComponent).IsAssignableFrom(t.Key)).Value;
                
            return _document.Content.Descendants(name).First();
        }

        public void Create<TComponent>() where TComponent : ILogixComponent
        {
            var name = Containers.FirstOrDefault(t => typeof(TComponent).IsAssignableFrom(t.Key)).Value;
            
            _document.Controller?.Add(new XElement(name));
        }
            
    }
}