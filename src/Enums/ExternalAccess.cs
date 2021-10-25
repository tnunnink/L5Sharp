using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class ExternalAccess : SmartEnum<ExternalAccess>
    {
        private ExternalAccess(string name, int value) : base(name, value)
        {
        }

        public abstract bool IsMoreRestrictive(ExternalAccess access);

        public static readonly ExternalAccess None = new NoneType();
        public static readonly ExternalAccess ReadOnly = new ReadOnlyType();
        public static readonly ExternalAccess ReadWrite = new ReadWriteType();

        private class NoneType : ExternalAccess
        {
            public NoneType() : base("None", 0)
            {
            }

            public override bool IsMoreRestrictive(ExternalAccess access)
            {
                return true;
            }
        }

        private class ReadOnlyType : ExternalAccess
        {
            public ReadOnlyType() : base("Read Only", 1)
            {
            }

            public override bool IsMoreRestrictive(ExternalAccess access)
            {
                return access != null && access.Equals(ReadWrite);
            }
        }

        private class ReadWriteType : ExternalAccess
        {
            public ReadWriteType() : base("Read/Write", 2)
            {
            }

            public override bool IsMoreRestrictive(ExternalAccess access)
            {
                return false;
            }
        }
    }
}