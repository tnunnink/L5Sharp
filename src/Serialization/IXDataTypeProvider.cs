using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    internal interface IXDataTypeProvider
    {
        /// <summary>
        /// Given an <see cref="XElement"/>, determine materialize a <see cref="IDataType"/> instance. 
        /// </summary>
        /// <param name="element">The element to process.</param>
        /// <returns>A new <see cref="IDataType"/> instance.</returns>
        IDataType FindType(XElement element);
    }
}