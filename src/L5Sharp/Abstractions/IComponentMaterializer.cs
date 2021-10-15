using System.Xml.Linq;

namespace L5Sharp.Abstractions
{
    internal interface IComponentMaterializer
    {
    }
    
    internal interface IComponentMaterializer<out T> : IComponentMaterializer where T : IComponent
    {
        T Materialize(XElement element);
    }
}