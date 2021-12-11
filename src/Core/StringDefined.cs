using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;

namespace L5Sharp.Core
{
    public class StringDefined : IStringDefined
    {
        public StringDefined(string name, Dimensions dimensions, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            
            if (dimensions.AreEmpty || dimensions.AreMultiDimensional) 
                throw new ArgumentException("Dimension must single dimensional and have length greater that zero");

            LEN = Member.Create<Dint>(nameof(LEN));
            DATA = ArrayMember.Create<Sint>(nameof(DATA), dimensions, Radix.Ascii);
            
            Members = new List<IMember<IDataType>>
            {
                LEN,
                DATA,
            };
        }

        public string Name { get; }
        public string Description { get; }
        public DataTypeFamily Family => DataTypeFamily.String;
        public DataTypeClass Class => DataTypeClass.User;
        public DataFormat Format => DataFormat.String;
        public string Value => GetValue();
        public IMember<Dint> LEN { get; }
        public IArrayMember<Sint> DATA { get; }
        public IStringDefined Update(string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMember<IDataType>> Members { get; }


        public void SetValue(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.DataType.Value)
                throw new ArgumentOutOfRangeException();
            
            ClearData();
            
            for (var i = 0; i < bytes.Length; i++)
                DATA[i].DataType.Update(bytes[i]);
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
                dataElement.DataType.Update(0);
        }
    }
}