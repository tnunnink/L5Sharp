using System;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Extensions;

namespace L5Sharp.Enums
{
    public abstract class ProgramType : SmartEnum<ProgramType, string>
    {
        private ProgramType(string name, string value) : base(name, value)
        {
        }

        public abstract IProgram Create(string name);

        public static T Create<T>(string name) where T : IProgram
        {
            return (T)Activator.CreateInstance(typeof(T),
                BindingFlags.CreateInstance |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.OptionalParamBinding, null, new[] {name, Type.Missing}, CultureInfo.CurrentCulture);
        }

        internal abstract IProgram Create(XElement element);

        public static readonly ProgramType Normal = new NormalType();
        public static readonly ProgramType EquipmentPhase = new EquipmentPhaseType();

        private class NormalType : ProgramType
        {
            public NormalType() : base(nameof(Normal), nameof(Normal))
            {
            }

            public override IProgram Create(string name)
            {
                return new Program(name);
            }

            internal override IProgram Create(XElement element)
            {
                throw new NotImplementedException();
            }

            /*internal override IProgram Create(XElement element)
            {
                return new Program(element.GetName(), element.GetDescription(), element.GetMainRoutineName(),
                    element.GetFaultRoutineName(), element.GetUseAsFolder(), element.GetTestEdits(),
                    element.GetDisabled());
            }*/
        }

        private class EquipmentPhaseType : ProgramType
        {
            public EquipmentPhaseType() : base(nameof(EquipmentPhase), nameof(EquipmentPhase))
            {
            }

            public override IProgram Create(string name)
            {
                throw new NotImplementedException();
            }

            internal override IProgram Create(XElement element)
            {
                throw new NotImplementedException();
            }
        }
    }
}