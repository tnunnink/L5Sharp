using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    public class StringDefined : LogixComponent, IStringDefined
    {
        public StringDefined(string name, Dimensions dimensions, string description = null) 
            : base(name, description)
        {
            if (dimensions.AreEmpty || dimensions.AreMultiDimensional) 
                throw new ArgumentException("Dimension must single dimensional and have length greater that zero");

            LEN = Member.Create<Dint>(nameof(LEN));
            DATA = Member.Create<Sint>(nameof(DATA), dimensions, Radix.Ascii);
            
            Members = new List<IMember<IDataType>>
            {
                LEN,
                DATA,
            };
        }

        public Radix Radix => Radix.Null;
        public DataTypeFamily Family => DataTypeFamily.String;
        public DataTypeClass Class => DataTypeClass.User;
        public TagDataFormat DataFormat => TagDataFormat.String;
        public string Value => GetValue();
        public IMember<Dint> LEN { get; }
        public IArrayMember<Sint> DATA { get; }
        public IEnumerable<IMember<IDataType>> Members { get; }

        public void SetValue(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException();
            
            ClearData();
            
            for (var i = 0; i < bytes.Length; i++)
                DATA[i].DataType.SetValue(bytes[i]);
        }

        public IDataType Instantiate()
        {
            return new StringDefined(Name, DATA.Dimensions.Copy(), Description);
        }

        public override string ToString()
        {
            return GetValue();
        }

        private string GetValue()
        {
            var bytes = DATA.Select(d => d.DataType.Value).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        private void ClearData()
        {
            foreach (var dataElement in DATA)
                dataElement.DataType.SetValue(0);
        }
    }
}