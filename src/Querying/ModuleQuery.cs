using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    internal class ModuleQuery : ComponentQuery<IModule>, IModuleQuery
    {
        public ModuleQuery(IEnumerable<XElement> elements, IL5XSerializer<IModule> serializer) 
            : base(elements, serializer)
        {
        }
        
        public IModule? Local() => Named("Local");
        
        public IModuleQuery WithParent(ComponentName parentName)
        {
            var results = Elements.Where(e =>
                e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == parentName.ToString());

            return new ModuleQuery(results, Serializer);
        }
    }
}