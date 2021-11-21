using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal interface IXSerializer
    {
    }
    
    internal interface IXSerializer<TComponent> : IXSerializer
    {
        XElement Serialize(TComponent component);
        
        TComponent Deserialize(XElement element);
    }
}