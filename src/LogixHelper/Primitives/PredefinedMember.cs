using LogixHelper.Enumerations;

namespace LogixHelper.Primitives
{
    public class ReadOnlyMember
    {
        private readonly DataTypeMember _member;

        public ReadOnlyMember(DataTypeMember member)
        {
            _member = member;
        }

        public string Name => _member.Name;
        public DataType DataType => _member.DataType;
        public string Description => _member.Description;
        public short Dimension => _member.Dimension;
        public Radix Radix => _member.Radix;
        


    }
}