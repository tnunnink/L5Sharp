using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal interface IComponentSerializer
    {
    }
    
    internal interface IComponentSerializer<in T> : IComponentSerializer where T : IComponent
    {
        XElement Serialize(T component);
    }
}