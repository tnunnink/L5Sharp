namespace LogixHelper
{
    public class DataValue
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public Radix Radix { get; set; }
        public object Value { get; set; }
        public object ForceValue { get; set; }
    }
}