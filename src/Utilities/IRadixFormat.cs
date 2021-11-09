using System;

namespace L5Sharp.Utilities
{
    public class RadixFormat : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}