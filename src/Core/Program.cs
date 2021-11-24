using System;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Program : IProgram
    {
        public Program(string name, string description = null, string mainRoutineName = null,
            string faultRoutineName = null, bool useAsFolder = false, bool testEdits = false, bool disabled = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            TestEdits = testEdits;
            Disabled = disabled;
            MainRoutineName = mainRoutineName;
            FaultRoutineName = faultRoutineName;
            UseAsFolder = useAsFolder;

            Tags = new Tags(this);
            Routines = new ComponentCollection<IRoutine>();
        }

        public ComponentName Name { get; }
        public string Description { get; }
        public ProgramType Type => ProgramType.Normal;
        public bool TestEdits { get; }
        public bool Disabled { get; }
        public ITags Tags { get; }
        public IComponentCollection<IRoutine> Routines { get; }
        public bool UseAsFolder { get; }
        public string MainRoutineName { get; }
        public string FaultRoutineName { get; }
    }
}