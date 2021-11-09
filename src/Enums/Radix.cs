using System;
using System.Text.RegularExpressions;
using Ardalis.SmartEnum;
using L5Sharp.Types;

namespace L5Sharp.Enums
{
    public abstract class Radix : SmartEnum<Radix, string>
    {
        private Radix(string name, string value) : base(name, value)
        {
        }

        public abstract string Format(IAtomic atomic);

        protected virtual string ConvertAtomic(IAtomic atomic, int baseNumber)
        {
            return atomic switch
            {
                Bool b => b ? "1" : "0", //todo can probably do better than this
                Sint s => Convert.ToString(s.Value, baseNumber),
                Int i => Convert.ToString(i.Value, baseNumber),
                Dint d => Convert.ToString(d.Value, baseNumber),
                Lint l => Convert.ToString(l.Value, baseNumber),
                _ => throw new NotSupportedException()
            };
        }

        public static readonly Radix Null = new NullRadix();
        public static readonly Radix General = new GeneralRadix();
        public static readonly Radix Binary = new BinaryRadix();
        public static readonly Radix Octal = new OctalRadix();
        public static readonly Radix Decimal = new DecimalRadix();
        public static readonly Radix Hex = new HexRadix();
        public static readonly Radix Exponential = new ExponentialRadix();
        public static readonly Radix Float = new FloatRadix();
        public static readonly Radix Ascii = new AsciiRadix();
        public static readonly Radix Unicode = new UnicodeRadix();
        public static readonly Radix DateTime = new DateTimeRadix();
        public static readonly Radix DateTimeNs = new DateTimeNsRadix();
        public static readonly Radix UseTypeStyle = new UseTypeStyleRadix();
        
        private class NullRadix : Radix
        {
            public NullRadix() : base("NullType", "NullType")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class GeneralRadix : Radix
        {
            public GeneralRadix() : base("General", "General")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class BinaryRadix : Radix
        {
            public BinaryRadix() : base("Binary", "Binary")
            {
            }

            public override string Format(IAtomic atomic)
            {
                var str = ConvertAtomic(atomic, 2);
                
                str = atomic switch
                {
                    Sint _ => str.PadLeft(8, '0'),
                    Int _ => str.PadLeft(16, '0'),
                    Dint _ => str.PadLeft(32, '0'),
                    Lint _ => str.PadLeft(64, '0'),
                    _ => throw new NotSupportedException()
                };
                
                str = Regex.Replace(str, ".{4}(?!$)", "$0_");
                return $"2#{str}";
            }
        }
        
        private class OctalRadix : Radix
        {
            public OctalRadix() : base("Octal", "Octal")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }

        private class DecimalRadix : Radix
        {
            public DecimalRadix() : base("Decimal", "Decimal")
            {
            }

            public override string Format(IAtomic atomic)
            {
                var str = ConvertAtomic(atomic, 10);
                
                //todo append correct specifier
                //todo Insert underscores?
                
                return str;
            }
        }
        
        private class HexRadix : Radix
        {
            public HexRadix() : base("Hex", "Hex")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class ExponentialRadix : Radix
        {
            public ExponentialRadix() : base("Exponential", "Exponential")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class FloatRadix : Radix
        {
            public FloatRadix() : base("Float", "Float")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class AsciiRadix : Radix
        {
            public AsciiRadix() : base("ASCII", "ASCII")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class UnicodeRadix : Radix
        {
            public UnicodeRadix() : base("Unicode", "Unicode")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class DateTimeRadix : Radix
        {
            public DateTimeRadix() : base("DateTime", "DateTime")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class DateTimeNsRadix : Radix
        {
            public DateTimeNsRadix() : base("DateTimeNs", "DateTimeNs")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
        
        private class UseTypeStyleRadix : Radix
        {
            public UseTypeStyleRadix() : base("UseTypeStyle", "UseTypeStyle")
            {
            }

            public override string Format(IAtomic atomic)
            {
                return string.Empty;
            }
        }
    }
}