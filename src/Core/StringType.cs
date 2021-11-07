using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class StringType : IDataType
    {
        private static readonly string[] MemberNames = { "LEN", "DATA" };
        private string _name;
        private string _description;

        private readonly Dictionary<string, Member<IDataType>> _members 
            = new Dictionary<string, Member<IDataType>>(StringComparer.OrdinalIgnoreCase);

        public StringType(string name, ushort length, string description = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (length <= 0)
                throw new ArgumentException("Length must be greater that 0");

            Name = name;
            Description = description ?? string.Empty;
            
            _members.Add(MemberNames[0],
                new Member<IDataType>(MemberNames[0], new Dint()));
            _members.Add(MemberNames[1],
                new Member<IDataType>(MemberNames[1], new Sint(), new Dimensions(length), Radix.Ascii));
        }

        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                Validate.DataTypeName(value);
                _name = value;
            }
        }

        public DataTypeFamily Family => DataTypeFamily.String;

        public DataTypeClass Class => DataTypeClass.User;

        public TagDataFormat DataFormat => TagDataFormat.String;

        public string Description
        {
            get => _description;
            private set => _description = value ?? string.Empty;
        }

        public Member<IDataType> Len => _members[nameof(Len)];
        public Member<IDataType> Data => _members[nameof(Data)];

        public IEnumerable<IMember<IDataType>> Members => _members.Values.AsEnumerable();

        public void UpdateLength(ushort length)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0");

            var data = new Member<IDataType>(MemberNames[1], new Sint(), new Dimensions(length), Radix.Ascii);
            
            _members.Remove(MemberNames[1]);
            _members.Add(data.Name, data);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}