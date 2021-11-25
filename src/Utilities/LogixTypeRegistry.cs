using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;

namespace L5Sharp.Utilities
{
    internal class LogixTypeRegistry
    {
        private readonly HashSet<LogixTypeRegistryItem> _registryItems = new HashSet<LogixTypeRegistryItem>();

        public LogixTypeRegistry(LogixContext context)
        {
            RegisterTypes(context.L5X.GetComponents<IDataType>().Elements(),
                DataTypeClass.User, 
                e => context.Serializer.Deserialize<IUserDefined>(e));
            
            //todo module defined
            //todo add on defined
        }

        public IDataType TryGetType(string name)
        {
            if (Logix.ContainsType(name))
                return Logix.InstantiateType(name);
            
            var item = _registryItems.SingleOrDefault(i => i.Name == name);
            return item != null ? item.Instantiate() : new Undefined();
        }

        private void RegisterTypes(IEnumerable<XElement> elements, DataTypeClass typeClass, 
            Func<XElement, IDataType> factory)
        {
            foreach (var element in elements)
                _registryItems.Add(new LogixTypeRegistryItem(element.GetName(), typeClass, element, factory));
        }
    }
}