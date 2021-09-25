using Ardalis.SmartEnum;

namespace L5Sharp.Enumerations
{
    public sealed class DataTypeFamily : SmartEnum<DataTypeFamily>
    {
        private DataTypeFamily(string name, int value) : base(name, value)
        {
        }

        public static readonly DataTypeFamily None = new DataTypeFamily("NoFamily", 0);
        public static readonly DataTypeFamily String = new DataTypeFamily("StringFamily", 1);
    }
}