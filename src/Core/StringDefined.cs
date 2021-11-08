using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class StringDefined : LogixComponent, IString
    {
        private readonly List<Member<IDataType>> _members = new List<Member<IDataType>>();

        public StringDefined(string name, ushort length, string description = null) 
            : base(name, description)
        {
            Validate.DataTypeName(name);
            
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (length <= 0) throw new ArgumentException("Length must be greater that 0");

            Name = name;

            LEN = new Dint();
            DATA = new Sint[length];
            
            _members.Add(new Member<IDataType>(nameof(LEN), new Dint()));
            _members.Add(new Member<IDataType>(nameof(DATA), new Sint(), new Dimensions(length), Radix.Ascii));
        }

        public string Name { get; }

        public DataTypeFamily Family => DataTypeFamily.String;

        public DataTypeClass Class => DataTypeClass.User;

        public TagDataFormat DataFormat => TagDataFormat.String;

        public string Description => string.Empty;

        public Dint LEN { get; }
        public Sint[] DATA { get; }

        public IEnumerable<IMember<IDataType>> Members => _members.AsEnumerable();


        public string GetValue()
        {
            var bytes = DATA.Select(d => d.Get()).ToArray();
            return Encoding.ASCII.GetString(bytes);
        }

        public void SetValue(string value)
        {
            var bytes = Encoding.ASCII.GetBytes(value);

            if (bytes.Length > LEN.Get())
                throw new ArgumentOutOfRangeException();

            for (var i = 0; i < bytes.Length; i++)
                DATA[i].Set(bytes[i]);
        }

        public void UpdateLength(ushort length)
        {
            /*if (length <= 0)
                throw new ArgumentException("Length must be greater than 0");

            var data = new Member<IDataType>(MemberNames[1], new Sint(), new Dimensions(length), Radix.Ascii);
            
            _members.Remove(MemberNames[1]);
            _members.Add(data.Name, data);*/
        }

        public override string ToString()
        {
            return GetValue();
        }
    }
}