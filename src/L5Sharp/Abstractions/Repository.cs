using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : IComponent
    {
        protected Repository(LogixContext context) : base(context)
        {
        }
        
        public virtual void Add(T component)
        {
            if (Container.Contains<T>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(T));
            
            Container.Add(component.Serialize());
        }

        public virtual void Remove(T component)
        {
            if (!Container.Contains<T>(component.Name)) return;
            var element = Container.GetSingle<T>(component.Name);
            element.Remove();
        }

        public virtual void Update(T component)
        {
            if (!Container.Contains<T>(component.Name))
                Container.Add(component.Serialize());
            
            var element = Container.GetSingle<T>(component.Name);
        }
    }
}