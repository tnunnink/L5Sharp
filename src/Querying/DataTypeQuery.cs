using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp.Serialization;

namespace L5Sharp.Querying
{
    public class DataTypeQuery : ComponentQuery<IComplexType>, IDataTypeQuery
    {
        protected DataTypeQuery(IEnumerable<XElement> elements, IL5XSerializer<IComplexType> serializer) 
            : base(elements, serializer)
        {
        }

        public IDataTypeQuery DependingOn(string typeName)
        {
            throw new System.NotImplementedException();
        }
    }
}