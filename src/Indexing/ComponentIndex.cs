using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.L5X;
using L5Sharp.Serialization;

namespace L5Sharp.Indexing
{
    internal class ComponentIndex<TComponent> : IL5XIndex<TComponent> where TComponent : ILogixComponent 
    {
        private readonly Dictionary<string, XElement> _index;
        private readonly IL5XSerializer<TComponent> _serializer;

        public ComponentIndex(L5XContext context)
        {
            _index = context.L5X.GetComponents<TComponent>()
                .Where(e => e.Attribute(L5XAttribute.Name.ToString()) is not null)
                .ToDictionary(e => e.ComponentName());
            
            _serializer = context.Serializer.GetFor<TComponent>();
        }
        
        public TComponent Lookup(string name) => _serializer.Deserialize(_index[name]);
    }
}