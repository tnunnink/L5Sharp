using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class KeyingState : SmartEnum<KeyingState>
    {
        private KeyingState(string name, int value) : base(name, value)
        {
        }

        public static readonly KeyingState ExactMatch = new KeyingState("ExactMatch", 0);
        public static readonly KeyingState CompatibleModule = new KeyingState("CompatibleModule", 1);
        public static readonly KeyingState Disabled = new KeyingState("Disabled", 2);
        public static readonly KeyingState Custom = new KeyingState("Custom", 3);
    }
}