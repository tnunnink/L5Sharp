using System.Xml.Linq;
using L5Sharp.Components;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Module"/> components.
    /// </summary>
    public class ModuleSerializer : ILogixSerializer<Module>
    {
        
        /// <inheritdoc />
        public XElement Serialize(Module obj)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Module Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}