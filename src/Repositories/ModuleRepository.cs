using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository for Logix <see cref="IModule"/> components.
    /// </summary>
    internal class ModuleRepository : Repository<IModule>, IModuleRepository
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleRepository"/> object.
        /// </summary>
        /// <param name="context">The logix context for which to read/write data to.</param>
        public ModuleRepository(L5XContext context) : base(context)
        {
        }

        public IModule? DeepFind(ComponentName name)
        {
            var module = Find(name);

            if (module is null) return null;

            var childNames = Context.L5X.GetComponents<IModule>()
                .Where(x => x.Attribute(nameof(module.ParentModule))?.Value == module.Name)
                .Select(x => x.ComponentName());

            foreach (var childName in childNames)
            {
                var child = DeepFind(childName);
                if (child is null) continue;
                module.Ports.Local()?.Bus?.Add(child);
            }

            return module;
        }

        public IModule DeepGet(ComponentName name)
        {
            var module = Get(name);
            
            var childNames = Context.L5X.GetComponents<IModule>()
                .Where(x => x.Attribute(nameof(module.ParentModule))?.Value == module.Name)
                .Select(x => x.ComponentName());
            
            foreach (var childName in childNames)
            {
                var child = DeepGet(childName);
                module.Ports.Local()?.Bus?.Add(child);
            }

            return module;
        }

        public IModule Local() => DeepGet("Local");

        public override void Add(IModule component)
        {
            base.Add(component);

            var bus = component.Ports.Local()?.Bus;
            if (bus is null) return;

            foreach (var module in bus.Where(m => m.Name != component.Name))
                Add(module);
        }

        public override void Remove(ComponentName name)
        {
            base.Remove(name);
            
            var children = Context.L5X.GetComponents<IModule>()
                .Where(x => x.Attribute("ParentModule")?.Value == name.ToString())
                .Select(x => x.ComponentName()).ToList();

            foreach (var child in children)
                Remove(child);
        }

        public override void Update(IModule component)
        {
            base.Update(component);
            
            var bus = component.Ports.Local()?.Bus;
            if (bus is null) return;

            foreach (var module in bus.Where(m => m.Name != component.Name))
                Update(module);
        }
    }
}