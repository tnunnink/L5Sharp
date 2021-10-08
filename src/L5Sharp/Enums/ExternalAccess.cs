using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ExternalAccess : SmartEnum<ExternalAccess>
    {
        private ExternalAccess(string name, int value) : base(name, value)
        {
        }
        
        public static readonly ExternalAccess None = new ExternalAccess("None", 0);
        public static readonly ExternalAccess ReadOnly = new ExternalAccess("Read Only", 1);
        public static readonly ExternalAccess ReadWrite = new ExternalAccess("Read/Write", 2);
    }
}