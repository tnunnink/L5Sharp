using System.Xml.Linq;
using L5Sharp.Core;

namespace L5Sharp.Serialization
{
    internal class CommentsSerializer : IXSerializer<Comments>
    {
        public XElement Serialize(Comments component)
        {
            throw new System.NotImplementedException();
        }

        public Comments Deserialize(XElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}