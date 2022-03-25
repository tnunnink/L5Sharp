using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Querying;
using L5Sharp.Serialization;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : ProgramQuery, IComponentRepository<IProgram>
    {
        public ProgramRepository(IEnumerable<XElement> elements, IL5XSerializer<IProgram> serializer) 
            : base(elements, serializer)
        {
        }

        public void Add(IProgram component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ComponentName name)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IProgram component)
        {
            throw new System.NotImplementedException();
        }
    }
}