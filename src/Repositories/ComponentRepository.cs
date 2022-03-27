using System;
using System.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    /// <summary>
    /// An abstract implementation of the <see cref="IComponentRepository{TComponent}"/> that provides the base
    /// functionality for adding, removing, and updating components on the <see cref="L5XContext"/>.
    /// </summary>
    /// <typeparam name="TComponent">The logix component type of the repository.</typeparam>
    internal abstract class ComponentRepository<TComponent> : ComponentQuery<TComponent>, IComponentRepository<TComponent>
        where TComponent : ILogixComponent
    {
        private readonly L5XDocument _document;
        
        protected ComponentRepository(L5XDocument document) 
            : base(document.Components.Get<TComponent>(), document.Serializers.ForComponent<TComponent>())
        {
            _document = document;
        }

        public void Add(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));
            
            if (!Elements.Any())
                _document.Containers.Create<TComponent>();

            if (Elements.Any(e => e.ComponentName() == component.Name))
                throw new ComponentNameCollisionException(component.Name, component.GetType());

            var element = Serializer.Serialize(component);

            Elements.Last().AddAfterSelf(element);
        }

        public void Remove(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (_document.Index.IsReferenced(component.Name))
                throw new ComponentReferencedException(nameof(component.Name), component.GetType());

            Elements.FirstOrDefault(x => x.ComponentName() == component.Name)?.Remove();
        }

        public void Update(TComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            var element = Serializer.Serialize(component);

            var current = Elements.FirstOrDefault(x => x.ComponentName() == component.Name);

            if (current is not null)
            {
                current.ReplaceWith(element);
                return;
            }
            
            if (!Elements.Any())
                _document.Containers.Create<TComponent>();

            Elements.Last().AddAfterSelf(element);
        }
    }
}