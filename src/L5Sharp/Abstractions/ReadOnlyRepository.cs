using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Abstractions
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : IComponent
    {
        protected readonly XElement Context;

        protected ReadOnlyRepository(XElement context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.GetAll<T>().Select(x => x.Deserialize<T>());
        }

        public virtual T Get(string name)
        {
            return Context.GetFirst<T>(name).Deserialize<T>();
        }
    }
}