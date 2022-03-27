using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    internal class ModuleRepository : ComponentRepository<IModule>, IModuleRepository
    {
        private readonly L5XDocument _document;

        public ModuleRepository(L5XDocument document) : base(document)
        {
            _document = document;
        }

        /*public void Add(IModule component)
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
                Add(module);#1#
        }*/

        /*public void Remove(ComponentName name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            Elements.FirstOrDefault(x => x.ComponentName() == name)?.Remove();

            var children = Elements
                .Where(x => x.Attribute(L5XAttribute.ParentModule.ToString())?.Value == name.ToString())
                .Select(x => x.ComponentName()).ToList();

            foreach (var child in children)
                Remove(child);
        }*/

        /*public void Update(IModule component)
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
                Update(module);#1#
        }*/
        
        public IModule? Local() => new ModuleQuery(Elements, Serializer).Local();

        public IModuleQuery WithParent(ComponentName parentName) =>
            new ModuleQuery(Elements, Serializer).WithParent(parentName);
    }
}