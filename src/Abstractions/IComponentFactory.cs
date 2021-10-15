using System.Xml.Linq;

namespace L5Sharp.Abstractions
{
    internal interface IComponentFactory
    {
        
    }
    
    internal interface IComponentFactory<out T> : IComponentFactory where T : IComponent
    {
        T Create(XElement element);
    }
}