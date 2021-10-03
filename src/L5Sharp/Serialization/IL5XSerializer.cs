using System.Runtime.CompilerServices;
using System.Xml.Linq;
using L5Sharp.Abstractions;

[assembly: InternalsVisibleTo("L5Sharp.Loaders.Tests")]

namespace L5Sharp.Serialization
{
    internal interface IL5XSerializer
    {
    }
    
    internal interface IL5XSerializer<T> : IL5XSerializer where T : IComponent
    {
        XElement Serialize(T component);
        
        T Deserialize(XElement element);
    }
}