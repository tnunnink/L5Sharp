using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Extensions;

namespace L5Sharp.Serialization
{
    public class RoutineSerializer : IComponentSerializer<IRoutine>
    {
        public XElement Serialize(IRoutine component)
        {
            throw new System.NotImplementedException();
        }
    }
}