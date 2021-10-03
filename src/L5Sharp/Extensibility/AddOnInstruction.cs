using System.Xml.Linq;
using L5Sharp.Abstractions;

namespace L5Sharp.Extensibility
{
    public abstract class AddOnInstruction : IInstruction
    {
        public string Name { get; }
        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}