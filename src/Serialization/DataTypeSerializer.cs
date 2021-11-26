using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    internal class DataTypeSerializer : IXSerializer<IDataType>
    {
        private readonly LogixContext _context;

        public DataTypeSerializer(LogixContext context)
        {
            _context = context;
        }
        
        public XElement Serialize(IDataType component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            
            var element = new XElement(LogixNames.Data);
            element.Add(component.ToAttribute(x => x.Format, "Format")); //todo should I change the property name?
            
            if (component is IAtomic atomic)
                element.Add(GenerateDataValue(atomic));

            return element;
        }

        public IDataType Deserialize(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var root = element.Elements().First();

            if (root.Name == LogixNames.DataValue)
                return Logix.InstantiateType(root.GetDataTypeName());

            //we can have array of any type. need to get type from context
            if (root.Name == LogixNames.Array)
            {
                
            }

            return new Undefined("Not sure");
        }

        private static XElement GenerateDataValue(IAtomic atomic)
        {
            var element = new XElement(LogixNames.DataValue);
            
            element.Add(atomic.ToAttribute(x => x.Name, LogixNames.DataType));
            element.Add(atomic.ToAttribute(x => x.Radix));
            element.Add(atomic.ToAttribute(x => x.Value));

            return element;
        }
    }
}