using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    internal class ProgramQuery : LogixQuery<IProgram>, IProgramQuery
    {
        public ProgramQuery(IEnumerable<XElement> source) : base(source)
        {
        }
    }
}