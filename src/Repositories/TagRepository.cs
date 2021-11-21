using System.Collections.Generic;
using System.Linq;
using L5Sharp.Extensions;

namespace L5Sharp.Repositories
{
    internal class TagRepository : ITagRepository
    {
        private readonly LogixContext _context;

        public TagRepository(LogixContext context)
        {
            _context = context;
        }

        public bool Exists(string name)
        {
            throw new System.NotImplementedException();
        }

        public ITag<IDataType> Get(string name)
        {
            var element = _context.L5X.Tags.Descendants().SingleOrDefault(t => t.GetName() == name);
            return _context.Serializer.Deserialize<ITag<IDataType>>(element);
        }

        public IEnumerable<ITag<IDataType>> GetAll()
        {
            return _context.L5X.Tags.Descendants().Select(e => _context.Serializer.Deserialize<ITag<IDataType>>(e));
        }

        public void Add(ITag<IDataType> component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ITag<IDataType> component)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ITag<IDataType> component)
        {
            throw new System.NotImplementedException();
        }
    }
}