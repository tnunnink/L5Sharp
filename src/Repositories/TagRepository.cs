using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Extensions;
using L5Sharp.L5X;

namespace L5Sharp.Repositories
{
    internal class TagRepository : Repository<ITag<IDataType>>, ITagRepository
    {
        public TagRepository(L5XContext context) : base(context)
        {
            Container = context.L5X.Content.Element(L5XElement.Controller.ToString())
                ?.Element(L5XElement.Tags.ToString()) ?? throw new ArgumentException();
            Components = Container.Descendants(L5XElement.Tag.ToString());
        }

        public TagRepository(L5XContext context, ComponentName program) : base(context)
        {
            if (program is null)
                throw new ArgumentNullException(nameof(program));

            var scope = context.L5X.Content
                .Descendants(L5XElement.Program.ToString())
                .FirstOrDefault(e => e.ComponentName() == program);

            if (scope is null) throw new ArgumentException();

            Container = scope.Element(L5XElement.Tags.ToString()) ?? throw new ArgumentException();
            Components = Container.Descendants(L5XElement.Tag.ToString());
        }

        public IEnumerable<ITag<TDataType>> WithType<TDataType>() where TDataType : IDataType
        {
            var typeName = typeof(TDataType).Name;

            var tags = Components.Where(t => string.Equals(t.Attribute(L5XAttribute.DataType.ToString())?.Value,
                typeName, StringComparison.OrdinalIgnoreCase));

            return tags.Select(e => Serializer.Deserialize(e)).Cast<ITag<TDataType>>();
        }
    }
}