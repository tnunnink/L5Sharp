using System.Xml.Linq;

namespace L5Sharp.Serialization.Data
{
    internal class AlarmDataSerializer : IL5XSerializer<IDataType>
    {
        public XElement Serialize(IDataType component)
        {
            throw new System.NotImplementedException();
        }

        public IDataType Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}