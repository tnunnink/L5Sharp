using LogixHelper.Enumerations;

namespace LogixHelper.Primitives
{
    public class PredefinedTypeMember
    {
        private readonly DataTypeMember _member;

        internal PredefinedTypeMember(DataTypeMember member)
        {
            _member = member;
        }

        public string Name => _member.Name;
        public DataType DataType => _member.DataType;
        public short Dimension => _member.Dimension;
        public Radix Radix => _member.Radix;
    }
}