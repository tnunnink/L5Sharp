using System.Collections.Generic;

namespace LogixHelper
{
    public class DataArray
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Dimensions { get; set; }
        public Radix Radix { get; set; }
        private IEnumerable<DataArrayElement> Elements { get; set; }
    }
}