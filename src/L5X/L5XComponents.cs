using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.L5X
{
    internal class L5XComponents
    {
        private static readonly Dictionary<Type, string> Components = new()
        {
            { typeof(IController), L5XElement.Controller.ToString() },
            { typeof(IComplexType), L5XElement.DataType.ToString() },
            { typeof(IModule), L5XElement.Module.ToString() },
            { typeof(IAddOnInstruction), L5XElement.AddOnInstructionDefinition.ToString() },
            { typeof(ITag<IDataType>), L5XElement.Tag.ToString() },
            { typeof(IProgram), L5XElement.Program.ToString() },
            { typeof(ITask), L5XElement.Task.ToString() }
        };

        private readonly L5XDocument _document;

        public L5XComponents(L5XDocument document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        /// <summary>
        /// Gets all <see cref="XElement"/> objects that represent the component element for specified component type.
        /// </summary>
        /// <typeparam name="TComponent">The <see cref="ILogixComponent"/> to get the component elements for.</typeparam>
        /// <returns>A new collection of <see cref="XElement"/> instances that represent the component element for the
        /// specified component type.</returns>
        public IEnumerable<XElement> Get<TComponent>() where TComponent : ILogixComponent
        {
            var name = Components.FirstOrDefault(t => typeof(TComponent).IsAssignableFrom(t.Key)).Value;

            if (name is null)
                throw new InvalidOperationException($"No component name defined for type '{typeof(TComponent)}'.");
            
            return _document.Content.Descendants(name)
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null);
        }
            
    }
}