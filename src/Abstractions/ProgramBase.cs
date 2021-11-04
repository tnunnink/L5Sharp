using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public abstract class ProgramBase : LogixComponent, IProgram
    {
        protected ProgramBase(string name, string description = null, bool testEdits = false, bool disabled = false)
            : base(name, description)
        {
            TestEdits = testEdits;
            Disabled = disabled;

            Tags = new Tags(this);
            Routines = new ComponentCollection<IRoutine>();
        }

        public abstract ProgramType Type { get; }

        public bool TestEdits { get; }

        public bool Disabled { get; private set; }

        public ITags Tags { get; }

        public IComponentCollection<IRoutine> Routines { get; }

        public void Enable()
        {
            Disabled = false;
        }

        public void Disable()
        {
            Disabled = true;
        }
    }
}