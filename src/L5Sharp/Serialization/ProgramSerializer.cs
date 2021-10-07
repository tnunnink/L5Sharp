using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;

[assembly: InternalsVisibleTo("L5Sharp.Serialization.Tests")]

namespace L5Sharp.Serialization
{
    internal class ProgramSerializer : IComponentSerializer<Program>
    {
        public XElement Serialize(Program component)
        {
            throw new System.NotImplementedException();
        }

        public Program Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}