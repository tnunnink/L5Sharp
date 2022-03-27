using L5Sharp.Core;
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

        public ITagQuery InProgram(ComponentName programName)
        {
            throw new System.NotImplementedException();
        }

        public ITagQuery WithType<TDataType>() where TDataType : IDataType
        {
            return new TagQuery(Elements, Serializer).WithType<TDataType>();
        }
    }
}