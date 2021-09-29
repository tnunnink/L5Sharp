using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Primitives;

namespace L5Sharp.Enumerations
{
    public class Radix : SmartEnum<Radix>
    {
        private Radix(string name, int value) : base(name, value)
        {
        }

        public static Radix Default(IDataType dataType)
        {
            return dataType.IsAtomic ? dataType.Equals(DataType.Real) ? Float : Decimal : Null;
        }

        public static readonly Radix Null = new Radix("NullType", 0);
        public static readonly Radix General = new Radix("General", 1); //todo what type does this go with
        public static readonly Radix Binary = new Radix("Binary", 2);
        public static readonly Radix Octal = new Radix("Octal", 3);
        public static readonly Radix Decimal = new Radix("Decimal", 4);
        public static readonly Radix Hex = new Radix("Hex", 5);
        public static readonly Radix Exponential = new Radix("Exponential", 6);
        public static readonly Radix Float = new Radix("Float", 7);
        public static readonly Radix Ascii = new Radix("ASCII", 8);
        public static readonly Radix Unicode = new Radix("Unicode", 9); //todo what type does this go with
        public static readonly Radix DateTime = new Radix("DateTime", 10);
        public static readonly Radix DateTimeNs = new Radix("DateTimeNs", 11);
        public static readonly Radix UseTypeStyle = new Radix("UseTypeStyle", 12); //todo what type does this go with
    }
}