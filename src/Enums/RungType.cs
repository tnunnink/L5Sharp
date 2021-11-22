using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class RungType : SmartEnum<RungType, string>
    {
        private RungType(string name, string value) : base(name, value)
        {
        }

        public static readonly RungType Normal = new RungType("N", "N");
        public static readonly RungType Insert = new RungType("I", "I");
        public static readonly RungType Delete = new RungType("D", "D");
        public static readonly RungType Replace = new RungType("R", "R");
        public static readonly RungType InsertReplace = new RungType("IR", "IR");
        public static readonly RungType PendingReplaceNormal = new RungType("rN", "rN");
        public static readonly RungType PendingInsertRung = new RungType("e", "e");
        public static readonly RungType PendingDeleteRung = new RungType("dN", "dN");
        public static readonly RungType PendingReplaceRung = new RungType("er", "er");
        public static readonly RungType PendingReplaceInsert = new RungType("rI", "rI");
        public static readonly RungType PendingReplace = new RungType("rR", "rR");
        public static readonly RungType dD = new RungType("dD", "dD");
        public static readonly RungType dI = new RungType("dI", "dI");
        public static readonly RungType dIR = new RungType("dIR", "dIR");
        
        
    }
}