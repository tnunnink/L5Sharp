using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : IComponent
    {
        protected Repository(XElement context) : base(context)
        {
        }

        public virtual void Add(T component)
        {
            if (Context.Contains<T>(component.Name))
                Throw.ComponentNameCollisionException(component.Name, typeof(T));
            
            Context.Add(component.Serialize());
        }

        public virtual void Remove(T component)
        {
            if (!Context.Contains<T>(component.Name))
                Throw.ComponentNotFoundException(component.Name, typeof(T));
            
            var element = Context.GetSingle<T>(component.Name);
            element.Remove();
        }

        public virtual void Update(T component)
        {
            if (!Context.Contains<T>(component.Name))
                Throw.ComponentNotFoundException(component.Name, typeof(T));
            
            var element = Context.GetSingle<T>(component.Name);
        }
    }
}