using System.Collections.Generic;

namespace LogixHelper
{
    public class Data
    {
        public string Format { get; set; }
        public IEnumerable<DataValue> DataValues { get; set; }
        public IEnumerable<DataStructure> Structures { get; set; }
        public IEnumerable<DataArray> Arrays { get; set; }
    }
}