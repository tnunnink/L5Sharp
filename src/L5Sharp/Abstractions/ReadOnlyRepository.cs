﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Abstractions
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : IComponent
    {
        protected readonly LogixContext Context;
        protected readonly XElement Container;
        internal readonly IComponentFactory<T> Factory;

        protected ReadOnlyRepository(LogixContext context)
        {
            Context = context;
            Container = context.Content.Container<T>();
            Factory = context.GetFactory<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Container.GetAll<T>().Select(e => Factory.Create(e));
        }

        public virtual T Get(string name)
        {
            return Factory.Create(Container.GetFirst<T>(name));
        }
    }
}