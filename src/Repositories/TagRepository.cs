using System;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Repositories
{
    internal class TagRepository : TagQuery, ITagRepository
    {
        private readonly L5XDocument _document;

        public TagRepository(L5XDocument document) 
            : base(document.Components.Get<ITag<IDataType>>(), document.Serializers.Get<TagSerializer>())
        {
            _document = document;
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