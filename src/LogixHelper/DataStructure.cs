using System.Collections.Generic;
using LogixHelper.Primitives;

namespace LogixHelper
{
    public class DataStructure
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public IEnumerable<DataValue> ValueMembers { get; set; }
        public IEnumerable<DataStructure> StructureMembers { get; set; }
        public IEnumerable<DataArray> ArrayMembers { get; set; }
    }
}