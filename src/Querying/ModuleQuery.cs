using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    /// <inheritdoc cref="L5Sharp.Querying.IModuleQuery" />
    public class ModuleQuery : ComponentQuery<IModule>, IModuleQuery
    {
        /// <inheritdoc />
        protected ModuleQuery(IEnumerable<XElement> elements, IL5XSerializer<IModule> serializer) 
            : base(elements, serializer)
        {
        }

        /// <inheritdoc />
        public IModule? Local() => Named("Local");

        /// <inheritdoc />
        public IModuleQuery WithParent(ComponentName parentName)
        {
            var results = Elements.Where(e =>
                e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == parentName.ToString());

            return new ModuleQuery(results, Serializer);
        }
    }
}