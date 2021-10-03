using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Primitives;
using L5Sharp.Utilities;

namespace L5Sharp.Extensibility
{
    public abstract class UserDefinedType : IDataType
    {
        private readonly Dictionary<string, Member> _members =
            new Dictionary<string, Member>();

        protected UserDefinedType(string name, string description = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            Validate.Name(name);
            Validate.DataTypeName(name);

            Name = name;
            Family = DataTypeFamily.None;
            Class = DataTypeClass.User;
            Description = description ?? string.Empty;
        }

        public string Name { get; }
        public DataTypeFamily Family { get; }
        public DataTypeClass Class { get; }
        public bool IsAtomic => false;
        public object Default => null;
        public string Description { get; }
        public IEnumerable<IMember> Members => _members.Values.Where(m => !m.Hidden).AsEnumerable();

        public bool SupportsRadix(Radix radix)
        {
            return false;
        }

        protected void RegisterMember(string name, IDataType dataType, string description = null,
            ushort dimension = 0, Radix radix = null, ExternalAccess access = null)
        {
            if (_members.ContainsKey(name))
                Throw.NameCollisionException(name, typeof(Member));

            var member = new Member(name, dataType, dimension, radix, access, description);

            if (dataType.Equals(DataType.Bool))
                GenerateBitBackingMember(member);

            _members.Add(member.Name, member);
        }

        private void GenerateBitBackingMember(Member member)
        {
            if (!member.DataType.Equals(DataType.Bool)) return;

            // ReSharper disable once StringLiteralTypo
            // All backing members have the appended string to prevent bit overlay error. 
            const string memberPrefix = "ZZZZZZZZZZ";

            // All backing members have a prefix number that is the position of the member in the collection.
            var memberIndex = (ushort)_members.Count;

            //All backing members follow this naming convention and are of type Sint and are hidden
            var backingMember = new Member($"{memberPrefix}{Name}{memberIndex}", DataType.Sint,
                0, Radix.Decimal, ExternalAccess.ReadWrite, string.Empty, true, string.Empty, 0);

            _members.Add(backingMember.Name, backingMember);

            member.Target = backingMember.Name;
        }
    }
}