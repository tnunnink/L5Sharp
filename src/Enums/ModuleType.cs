using System;
using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public abstract class ModuleType : SmartEnum<ModuleType>
    {
        private ModuleType(string name, int value) : base(name, value)
        {
        }

        public static ModuleType FromCode(string code)
        {
            throw new NotImplementedException();
        }

        public static readonly ModuleType Analog = new AnalogType();
        public static readonly ModuleType Digital = new AnalogType();
        public static readonly ModuleType Communication = new AnalogType();
        public static readonly ModuleType Controller = new AnalogType();
        public static readonly ModuleType Motion = new AnalogType();
        public static readonly ModuleType Specialty = new AnalogType();

        private class AnalogType : ModuleType
        {
            public AnalogType() : base(nameof(Analog), 0)
            {
            }
        }
    }
}