using System;
using System.Xml.Linq;
using L5Sharp.Common;
using L5Sharp.Core;
using L5Sharp.Serialization.Data;

namespace L5Sharp.Serialization.Providers
{
    /// <summary>
    /// An <see cref="ArrayElementTypeProvider"/> expects an element of a data structure array.
    /// An array can either be a value type array, in which it has a value attribute and a parent Array element to
    /// determine an atomic type from, or it can be a complex structure array, in which it has a child structure element
    /// that can be parsed into a complex type using a structure serializer.
    /// </summary>
    internal class ArrayElementTypeProvider : IXDataTypeProvider
    {
        public IDataType FindType(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var structure = element.Element(LogixNames.Structure);

            if (structure is not null)
            {
                var serializer = new StructureSerializer();
                return serializer.Deserialize(structure);
            }

            var atomicType = (IAtomicType)DataType.New(element.Parent?.Attribute(LogixNames.DataType)?.Value!);
            var value = element.Attribute(LogixNames.Value)?.Value!;
            atomicType.SetValue(value);
            return atomicType;
        }
    }
}