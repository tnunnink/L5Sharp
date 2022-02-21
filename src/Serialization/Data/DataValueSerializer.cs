﻿using System;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Helpers;

namespace L5Sharp.Serialization.Data
{
    internal class DataValueSerializer : IL5XSerializer<IAtomicType>
    {
        private static readonly XName ElementName = L5XElement.DataValue.ToXName();
        
        public XElement Serialize(IAtomicType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            var element = new XElement(ElementName);

            element.AddAttribute(component, m => m.Name, nameOverride: L5XElement.DataType.ToString());
            //todo what about radix...
            element.Add(new XAttribute(L5XAttribute.Value.ToXName(), component.Format()));

            return element;
        }

        public IAtomicType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (element.Name != ElementName)
                throw new ArgumentException($"Element '{element.Name}' not valid for the serializer {GetType()}.");
            
            var atomic = (IAtomicType)DataType.Create(element.GetDataTypeName());
            //todo what about radix...
            var radix = element.GetAttribute<IMember<IDataType>, Radix>(m => m.Radix) ?? Radix.Default(atomic);
            var value = element.Attribute(L5XAttribute.Value.ToXName())?.Value!;
            
            atomic.SetValue(value);

            return atomic;
        }
    }
}