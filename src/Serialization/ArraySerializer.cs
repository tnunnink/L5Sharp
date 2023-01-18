using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A <see cref="ILogixSerializer{T}"/> that serializes <see cref="ArrayType{TLogixType}"/> or array data elements.
    /// </summary>
    public class ArraySerializer : ILogixSerializer<ArrayType<ILogixType>>
    {
        private readonly ArrayElementSerializer _arrayElementSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(ArrayType<ILogixType> obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Array);
            element.Add(new XAttribute(L5XName.DataType, obj.Name));
            element.Add(new XAttribute(L5XName.Dimensions, obj.Dimensions));
            element.Add(obj.Members().Select(m => _arrayElementSerializer.Serialize(m)));

            return element;
        }

        /// <inheritdoc />
        public ArrayType<ILogixType> Deserialize(XElement element)
        {
            Check.NotNull(element);

            var dimensions = element.Attribute(L5XName.Dimensions)?.Value.Parse<Dimensions>()
                             ?? throw new ArgumentException($"Element must have {L5XName.Dimensions} attribute.");

            var elements = element.Elements().Select(e => _arrayElementSerializer.Deserialize(e));

            //return new ArrayType<ILogixType>(dimensions, elements);
            throw new NotImplementedException();
        }
    }
}
