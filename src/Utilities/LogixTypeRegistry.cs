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
            RegisterTypes(context.L5X.GetComponents<IDataType>(),
                DataTypeClass.User, 
                e => context.Serialization.Deserialize<IUserDefined>(e));
            
            /*RegisterTypes(context.L5X.GetComponents<IModuleDefined>(),
                DataTypeClass.Io, 
                e => context.Serialization.Deserialize<IModuleDefined>(e));*/
            
            RegisterTypes(context.L5X.GetComponents<IAddOnInstruction>(),
                DataTypeClass.AddOnDefined, 
                e => context.Serialization.Deserialize<IAddOnInstruction>(e));
        }

        public IDataType TryGetType(string name)
        {
            if (Logix.ContainsType(name))
                return Logix.InstantiateType(name);
            
            var item = _registryItems.SingleOrDefault(i => i.Name == name);
            return item != null ? item.Instantiate() : new Undefined(name);
        }

        private void RegisterTypes(IEnumerable<XElement> elements, DataTypeClass typeClass, 
            Func<XElement, IDataType> factory)
        {
            foreach (var element in elements)
                _registryItems.Add(new LogixTypeRegistryItem(element.GetName(), typeClass, element, factory));
        }
    }
}