using L5Sharp.Enums;

namespace L5Sharp.Abstractions
{
    public abstract class ProgramBase : LogixComponent, IProgram
    {
        private bool _disabled;
        
        protected ProgramBase(string name, string description = null, bool testEdits = false, bool disabled = false)
            : base(name, description)
        {
            TestEdits = testEdits;
            _disabled = disabled;

            Tags = new ComponentCollection<ITag>();
            Routines = new ComponentCollection<IRoutine>();
        }

        public abstract ProgramType Type { get; }

        public bool TestEdits { get; }

        public bool Disabled => _disabled;

        public IComponentCollection<ITag> Tags { get; }

        public IComponentCollection<IRoutine> Routines { get; }

        public void Enable()
        {
            SetProperty(ref _disabled, false, nameof(Disabled));
        }

        public void Disable()
        {
            SetProperty(ref _disabled, true, nameof(Disabled));
        }
    }
}