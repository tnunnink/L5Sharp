using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository for Logix <see cref="IModule"/> components.
    /// </summary>
    internal class ModuleRepository : ComponentRepository<IModule>, IModuleRepository
    {
        public ModuleRepository(IEnumerable<XElement> elements, IL5XSerializer<IModule> serializer)
            : base(elements, serializer)
        {
        }

        public IModule? Local() => SingleOrDefault("Local");

        public IModuleQuery WithParent(ComponentName parentName)
        {
            var results = Elements.Where(e =>
                e.Attribute(L5XAttribute.ParentModule.ToString())?.Value == parentName.ToString());

            return new ModuleRepository(results, Serializer);
        }

        public override void Add(IModule component)
        {
            base.Add(component);

            var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Add(module);
        }

        public override void Remove(ComponentName name)
        {
            base.Remove(name);

            var children = Elements
                .Where(x => x.Attribute("ParentModule")?.Value == name.ToString())
                .Select(x => x.ComponentName()).ToList();

            foreach (var child in children)
                Remove(child);
        }

        public override void Update(IModule component)
        {
            base.Update(component);

            var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Update(module);
        }
    }
}