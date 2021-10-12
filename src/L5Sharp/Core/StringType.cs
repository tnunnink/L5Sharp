using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class StringType : IDataType
    {
        private static readonly string[] MemberNames = { "LEN", "DATA" };
        private string _name;
        private string _description;

        private readonly Dictionary<string, ReadOnlyMember> _members = new Dictionary<string, ReadOnlyMember>();

        public StringType(string name, ushort length, string description = null)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            if (length <= 0)
                throw new ArgumentException("Length must be greater that 0");

            Name = name;
            Description = description ?? string.Empty;
            
            _members.Add(MemberNames[0],
                new ReadOnlyMember(MemberNames[0], Predefined.Dint));
            _members.Add(MemberNames[1],
                new ReadOnlyMember(MemberNames[1], Predefined.Sint, length, Radix.Ascii));
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

        public bool IsAtomic => false;

        public object DefaultValue => string.Empty;

        public Radix DefaultRadix => Radix.Null;

        public TagDataFormat DataFormat => TagDataFormat.String;

        public string Description
        {
            get => _description;
            set => _description = value ?? string.Empty;
        }

        public IMember Len => Members.SingleOrDefault(x => x.Name == nameof(Len).ToUpper());
        public IMember Data => Members.SingleOrDefault(x => x.Name == nameof(Data).ToUpper());

        public IEnumerable<IMember> Members => _members.Values.AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return radix == Radix.Null;
        }

        public bool IsValidValue(object value)
        {
            return value is string;
        }

        public IMember GetMember(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDataType> GetDependentTypes()
        {
            throw new NotImplementedException();
        }

        public void UpdateLength(ushort length)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0");

            var data = new ReadOnlyMember(MemberNames[1], Predefined.Sint, length, Radix.Ascii);
            
            _members.Remove(MemberNames[1]);
            _members.Add(data.Name, data);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}