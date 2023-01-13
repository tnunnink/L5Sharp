using System.Xml.Linq;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogixDeserializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Deserialize(XElement element);
    }
}