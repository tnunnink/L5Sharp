using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming

namespace L5Sharp.Types
{
    public class String : IString
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix
        
        public String(string value = default)
        {
            LEN = new Dint();
            DATA = new Sint[PredefinedLength];

            Members = new List<IMember<IDataType>>
            {
                new Member<IDataType>(nameof(LEN), new Dint()),
                new Member<IDataType>(nameof(DATA), new Sint(), new Dimensions(PredefinedLength), Radix.Ascii)
            };

            if (!string.IsNullOrEmpty(value))
                SetValue(value);
        }

        public string Name => nameof(String);

        public DataTypeFamily Family => DataTypeFamily.String;

        public DataTypeClass Class => DataTypeClass.Predefined;

        public TagDataFormat DataFormat => TagDataFormat.String;

        public string Description => string.Empty;
        public IEnumerable<IMember<IDataType>> Members { get; }
        public Dint LEN { get; }
        public Sint[] DATA { get; }

        public string GetValue()
        {
            var bytes = DATA.Select(d => d.Get()).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        public void SetValue(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > PredefinedLength)
                throw new ArgumentOutOfRangeException();

            for (var i = 0; i < bytes.Length; i++)
                DATA[i].Set(bytes[i]);
        }

        public static implicit operator String(string input)
        {
            return new String(input);
        }
        
        public static implicit operator string(String input)
        {
            return input.GetValue();
        }
    }
}