using System.Collections.Generic;

namespace LogixHelper
{
    public class DataStructure
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public IEnumerable<DataValue> ValueMembers { get; set; }
        public IEnumerable<DataStructure> StructureMembers { get; set; }
        public IEnumerable<DataArray> ArrayMembers { get; set; }
    }
}