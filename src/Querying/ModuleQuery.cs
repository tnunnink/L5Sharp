using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;

namespace L5Sharp.Querying
{
    internal class ModuleQuery : LogixQuery<IModule>, IModuleQuery
    {
        public ModuleQuery(IEnumerable<XElement> source) : base(source)
        {
        }

        public IModuleQuery WithParent(ComponentName parentName)
        {
            if (parentName is null)
                throw new ArgumentNullException(nameof(parentName));

            var results = this.Where(e =>
                e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == parentName.ToString());

            return new ModuleQuery(results);
        }
    }
}