using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Repositories
{
    public class Repository<T> : IRepository<T> where T : IComponent
    {
        protected readonly LogixContext Context;
        protected readonly XElement Container;

        protected Repository(LogixContext context)
        {
            Context = context;
            Container = context.Content.Container<T>();
        }
        
        public virtual IEnumerable<T> GetAll()
        {
            return Container.GetAll<T>().Select(x => x.Deserialize<T>());
        }

        public virtual T Get(string name)
        {
            return Context.Content.GetSingle<T>(name).Deserialize<T>();
        }

        public virtual void Add(T component)
        {
            if (Container.Contains<T>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(T));
            
            Context.Content.Container<T>().Add(component.Serialize());
        }

        public virtual void Remove(T component)
        {
            if (!Container.Contains<T>(component.Name))
                Throw.ComponentNotFoundException(component.Name, typeof(T));
            
            var element = Context.Content.GetSingle<T>(component.Name);
            element.Remove();
        }

        public virtual void Update(T component)
        {
            if (!Container.Contains<T>(component.Name))
                Throw.ComponentNotFoundException(component.Name, typeof(T));
            
            var element = Context.Content.GetSingle<T>(component.Name);
        }
    }
}