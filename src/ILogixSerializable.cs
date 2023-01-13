using System.Xml.Linq;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogixSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        XElement Serialize();
    }
}