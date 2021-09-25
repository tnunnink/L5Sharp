using Ardalis.SmartEnum;

namespace LogixHelper.Enumerations
{
    public class DataTypeClass : SmartEnum<DataTypeClass>
    {
        private DataTypeClass(string name, int value) : base(name, value)
        {
        }

        public static readonly DataTypeClass User = new DataTypeClass("User", 0);
        public static readonly DataTypeClass Predefined = new DataTypeClass("ProductDefined", 1); //I think this mean predefined 
        public static readonly DataTypeClass Io = new DataTypeClass("IO", 2);
    }
}