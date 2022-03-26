using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : ProgramQuery, IProgramRepository
    {
        private readonly L5XDocument _document;

        public ProgramRepository(L5XDocument document) 
            : base(document.Components.Get<IProgram>(), document.Serializers.Get<ProgramSerializer>())
        {
            _document = document;
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