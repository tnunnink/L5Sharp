using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal class ModuleSerializer : IXSerializer<IModule>
    {
        public XElement Serialize(IModule component)
        {
            throw new System.NotImplementedException();
        }

        public IModule Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}