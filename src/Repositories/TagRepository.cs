using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class TagRepository : TagQuery, ITagRepository
    {
        public TagRepository(IEnumerable<XElement> elements, IL5XSerializer<ITag<IDataType>> serializer) 
            : base(elements, serializer)
        {
        }

        /*public TagRepository(L5XContext context, ComponentName program) : base(context)
        {
            if (program is null)
                throw new ArgumentNullException(nameof(program));

            var scope = context.L5X.Content
                .Descendants(L5XElement.Program.ToString())
                .FirstOrDefault(e => e.ComponentName() == program);

            if (scope is null) throw new ArgumentException();

            Container = scope.Element(L5XElement.Tags.ToString()) ?? throw new ArgumentException();
            Components = Container.Descendants(L5XElement.Tag.ToString());
        }*/
        
        public void Add(ITag<IDataType> component)
        {
            throw new NotImplementedException();
        }

        public void Remove(ComponentName name)
        {
            throw new NotImplementedException();
        }

        public void Update(ITag<IDataType> component)
        {
            throw new NotImplementedException();
        }
    }
}