using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;

[assembly: InternalsVisibleTo("L5Sharp.Loaders.Tests")]

namespace L5Sharp.Serialization
{
    internal interface IComponentSerializer
    {
    }
    
    internal interface IComponentSerializer<T> : IComponentSerializer where T : IComponent
    {
        XElement Serialize(T component);
        
        T Deserialize(XElement element);
    }
}