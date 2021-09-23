using Ardalis.SmartEnum;

namespace LogixHelper.Enumerations
{
    public class DataTypeClass : SmartEnum<DataTypeClass>
    {
        private DataTypeClass(string name, int value) : base(name, value)
        {
        }

        public static readonly DataTypeClass User = new DataTypeClass("User", 0);
        public static readonly DataTypeClass ProductDefined = new DataTypeClass("ProductDefined", 1);
        public static readonly DataTypeClass Io = new DataTypeClass("IO", 2);
    }
}