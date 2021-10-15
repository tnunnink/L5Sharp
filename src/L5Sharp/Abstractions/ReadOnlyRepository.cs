using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Abstractions
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : IComponent
    {
        protected readonly LogixContext Context;
        protected readonly XElement Container;

        protected ReadOnlyRepository(LogixContext context)
        {
            Context = context;
            Container = context.Content.Container<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            var cache = Context.GetCache<T>();
            return Container.GetAll<T>()
                .Select(e => cache.GetOrCreate(e.GetName(), () => e.Deserialize<T>(Context)));
        }

        public virtual T Get(string name)
        {
            var cache = Context.GetCache<T>();
            return cache.GetOrCreate(name, () => Container.GetFirst<T>(name).Deserialize<T>(Context));
        }
    }
}