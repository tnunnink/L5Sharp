using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class Use : SmartEnum<Use>
    {
        public Use(string name, int value) : base(name, value)
        {
        }

        public static readonly Use Invalid = new Use("Invalid", 0);
        public static readonly Use Context = new Use("Context", 1);
        public static readonly Use Create = new Use("Create", 2);
        public static readonly Use Target = new Use("Target", 3);
        public static readonly Use Update = new Use("Update", 4);
        public static readonly Use Delete = new Use("Delete", 5);
        public static readonly Use Insert = new Use("Insert", 6);
        public static readonly Use Append = new Use("Append", 7);
        public static readonly Use Redefine = new Use("Redefine", 8);
        public static readonly Use Reference = new Use("Reference", 9);
        public static readonly Use Overwrite = new Use("Overwrite", 10);
    }
}