using System;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Repositories
{
    internal class ModuleRepository : ModuleQuery, IModuleRepository
    {
        private readonly L5XDocument _document;

        public ModuleRepository(L5XDocument document)
            : base(document.Components.Get<IModule>(), document.Serializers.Get<ModuleSerializer>())
        {
            _document = document;
        }

        public void Add(IModule component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            if (Elements.Any(e => e.ComponentName() == component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Elements.Last().AddAfterSelf(element);

            _document.Index.Run();
            /*var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Add(module);*/
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
            
            _document.Index.Run();
            /*var modules = component.Bus.Modules();

            foreach (var module in modules.Where(m => m.Name != component.Name))
                Update(module);*/
        }
    }
}