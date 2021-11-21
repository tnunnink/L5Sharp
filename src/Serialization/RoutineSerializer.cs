using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    public class RoutineSerializer : IXSerializer<IRoutine>
    {
        public XElement Serialize(IRoutine component)
        {
            throw new System.NotImplementedException();
        }

        public IRoutine Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}