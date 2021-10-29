using System.Xml.Linq;

namespace L5Sharp.Factories
{
    internal interface IComponentFactory
    {
        
    }
    
    internal interface IComponentFactory<out T> : IComponentFactory where T : IComponent
    {
        T Create(XElement element);
    }
}