using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal interface IComponentMaterializer
    {
        
    }
    
    internal interface IComponentMaterializer<out T> : IComponentMaterializer where T : ILogixComponent
    {
        T Materialize(XElement element);
    }
}