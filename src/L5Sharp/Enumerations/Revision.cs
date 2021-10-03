using Ardalis.SmartEnum;
// ReSharper disable InconsistentNaming

namespace L5Sharp.Enumerations
{
    public class Revision : SmartEnum<Revision>
    {
        private Revision(string name, int value) : base(name, value)
        {
        }
        
        public int Major { get; }
        public int Minor { get; }

        public static readonly Revision v32 = new Revision(nameof(v32), 32);
    }
}