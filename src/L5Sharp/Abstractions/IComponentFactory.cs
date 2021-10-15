using System.Xml.Linq;

namespace L5Sharp.Abstractions
{
    public interface IComponentFactory
    {
        
    }
    
    internal interface IComponentFactory<out T> : IComponentFactory where T : IComponent
    {
        T Create(XElement element);
    }
}