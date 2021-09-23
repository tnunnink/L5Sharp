using LogixHelper.Enumerations;

namespace LogixHelper.Primitives
{
    public class DataValue
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public string Description { get; set; }
        public Radix Radix { get; set; }
        public object Value { get; set; }
        public object ForceValue { get; set; }
    }
}