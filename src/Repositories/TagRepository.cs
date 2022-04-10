using L5Sharp.L5X;
using L5Sharp.Querying;

namespace L5Sharp.Repositories
{
    internal class TagRepository : ComponentRepository<ITag<IDataType>>, ITagRepository
    {
        private readonly L5XDocument _document;

        public TagRepository(L5XDocument document) : base(document)
        {
            _document = document;
        }

        public ITagQuery WithDataType(string typeName) => new TagQuery(Elements, Serializer).WithDataType(typeName);
    }
}