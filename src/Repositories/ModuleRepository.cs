using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository for Logix <see cref="IModule"/> components.
    /// </summary>
    internal class ModuleRepository : ModuleQuery, IModuleRepository
    {
        public ModuleRepository(IEnumerable<XElement> elements, IL5XSerializer<IModule> serializer)
            : base(elements, serializer)
        {
        }

        public void Add(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Elements.Any(e => e.ComponentName() == component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Elements.Last().AddAfterSelf(element);

            var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Add(module);
        }

        public void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            Elements.FirstOrDefault(x => x.ComponentName() == name)?.Remove();

            var children = Elements
                .Where(x => x.Attribute(L5XAttribute.ParentModule.ToString())?.Value == name.ToString())
                .Select(x => x.ComponentName()).ToList();

            foreach (var child in children)
                Remove(child);
        }

        public void Update(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = Serializer.Serialize(component);

            var current = Elements.FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }

            Elements.Last().AddAfterSelf(element);

            var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Update(module);
        }
    }
}