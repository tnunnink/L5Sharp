using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Core;
using L5Sharp.Enums;

// ReSharper disable InconsistentNaming RSLogix Naming

namespace L5Sharp.Types
{
    public class String : IString
    {
        private const int PredefinedLength = 82; //This is the built in length of string types in RSLogix
        
        public String()
        {
            LEN = Member.OfType<Dint>(nameof(LEN));
            DATA = Member.OfType<Sint>(nameof(DATA), new Dimensions(PredefinedLength), Radix.Ascii);
            
            Members = new List<IMember<IDataType>>
            {
                (IMember<IDataType>)LEN,
                (IMember<IDataType>)DATA,
            };
        }

        private String(string value = default) : this()
        {
            if (!string.IsNullOrEmpty(value))
                Set(value);
        }

        public string Name => nameof(String).ToUpper();
        public DataTypeFamily Family => DataTypeFamily.String;
        public DataTypeClass Class => DataTypeClass.Predefined;
        public TagDataFormat DataFormat => TagDataFormat.String;
        public string Description => string.Empty;
        public IEnumerable<IMember<IDataType>> Members { get; }
        public IMember<Dint> LEN { get; }
        public IMember<Sint> DATA { get; }

        public string Get()
        {
            var bytes = DATA.Elements.Select(d => d.DataType.Get()).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        public void Set(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Get())
                throw new ArgumentOutOfRangeException();
            
            ClearData();
            
            for (var i = 0; i < bytes.Length; i++)
                DATA.Elements[i].DataType.Set(bytes[i]);
        }

        public static implicit operator String(string input)
        {
            return new String(input);
        }
        
        public static implicit operator string(String input)
        {
            return input.Get();
        }

        private void ClearData()
        {
            foreach (var dataElement in DATA.Elements)
                dataElement.DataType.Set(0);
        }
    }
}